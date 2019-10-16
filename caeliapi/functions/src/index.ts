import * as functions from 'firebase-functions';

import * as admin from "firebase-admin";
admin.initializeApp();

import * as express from "express";
import * as crypto from "crypto";
import * as cors from "cors";
import {checkIfAuthenticated} from "./auth";
import {SUPPORTED_REGIONS} from "firebase-functions";
const app = express();



app.use(checkIfAuthenticated);
app.use(express.json());
app.use(cors());

const worldcollection = admin.firestore().collection("worlds");


app.get('/worlds', async (req: any, res: any) => {
    // get worlds by authId/ owner of world
    const querySnapshot = await worldcollection.where("authId","==", req['authId']).get();

    const listOfWorlds: any[] = [];

    querySnapshot.forEach((result) => {
        let docData = result.data();

        listOfWorlds.push(docData);
    })

    res.send(JSON.stringify(listOfWorlds));
})

interface World {
    authId?: string;
    id?: string;
    shareCode?: string;
    world: {
        WorldData: any[][]; // n x n tiles
        ResourceData: {
            Money: number;
            Population: number;
            [other: string]: any; // resource model may change before final deadline.
        }; // resource data
        IsTutorialFinished: boolean;
    }
}

app.post("/worlds",  async (req: any, res: any) => {
    const world: World = req.body;

    if(world.id || world.shareCode || world.authId || !world.world) {
        return res.status(400).send("Invalid New World Format");
    }

    world.authId = req['authId'];

    try {
        const newWorld = await worldcollection.add(world);
        res.status(201).send(newWorld.id);
    } catch (err) {
        res.status(500).send();
    }
})


app.put("/worlds/:id", async (req: any , res:any) => {
    const updatedWorld: World = req.body;

    if(!updatedWorld.world) {
        return res.status(400).send("Invalid Updated World Format");
    }

    try {
        const worldDoc = worldcollection.doc(req.params.id);
        const world = await worldDoc.get();
        const worldData =  world.data();

        if (!world.exists || !worldData)  {
           return res.status(404).send();
        }


        if(worldData['authId'] != req['authId']) {
            return res.status(401).send();
        }

        await worldDoc.set({
            world: updatedWorld.world
        })

        return res.status(204).send();

    } catch (err) {
        res.status(500).send();
    }


})

app.delete("/worlds/:id", async (req: any, res: any) => {
    // retrieve world.
    const worldDoc = worldcollection.doc(req.params.id);
    const world = await worldDoc.get();
    const worldData = world.data();

    if (!world.exists || !worldData)  {
        return res.status(404).send();
    }

    if (worldData['authId'] != req['authId']) {
        return res.status(401).send();
    }

    await worldDoc.delete();

    res.status(204).send();
})

app.post("/worlds/:id/share", async (req:any ,res:any) => {
    // retrieve world at id, if auth id matches continue
    const worldDoc = worldcollection.doc(req.params.id);
    const world = await worldDoc.get();
    const worldData = world.data();

    if (!world.exists || !worldData)  {
        return res.status(404).send();
    }

    if (worldData['authId'] != req['authId']) {
        return res.status(401).send();
    }

    if (worldData['shareCode']) {   // if world has a share code, return the share code
        return res.status(200).send(worldData['shareCode']);
    } else {
        // if world does not have a sharecode,
        // generate a cool unique code for the user
        // save code to db
        // return code
        const newShareCode = await generateUniqueShareCode();

        await worldDoc.set({
            shareCode: newShareCode
        });

        return res.status(200).send(newShareCode);
    }



})

async function generateUniqueShareCode(): Promise<string> {

    const shareCode  = crypto.randomBytes(4).toString('hex');

    const potentialExisting = await worldcollection.where("shareCode","==", shareCode).get();

    if (!potentialExisting.empty) { // re-generate if id exists in db.
        return generateUniqueShareCode();
    }

    return shareCode;

}


app.get("/sharedworlds/:shareCode", async (req: any, res: any) => {

    const potentialExisting = await worldcollection.where("shareCode","==", req.params['shareCode']).get();

    if (!potentialExisting.empty) {
        return res.status(404).send();
    }

    res.status(200).send(potentialExisting.docs[0]); // assume only one doc exists
})


// I have learned that firebase does not support cloud functions in sydney yet, oh well.
exports.api = functions.region(SUPPORTED_REGIONS[2]).https.onRequest(app);