import * as firebase from 'firebase-admin';
import * as functions from 'firebase-functions';

import * as express from "express";
import * as crypto from "crypto";
import * as cors from "cors";
import * as morgan from "morgan";
import {checkIfAuthenticated} from "./auth";
import {SUPPORTED_REGIONS} from "firebase-functions";
import {firebaseApp} from "./firebase";
const app = express();



app.use(checkIfAuthenticated);
app.use(express.json());
app.use(cors());
app.use(morgan('tiny'));

const worldcollection = firebaseApp.firestore().collection("worlds");


app.get("/worlds", async (req: any, res: any) => {
    // get worlds by authId/ owner of world
    console.log(req);
    console.log('get')
    const querySnapshot = await worldcollection.where("authId","==", req['authId']).get();

    const listOfWorlds: any[] = [];

    querySnapshot.forEach((result) => {
        let docData = result.data();
        docData.id = result.id;
        listOfWorlds.push(docData);
    })

    res.send(listOfWorlds);
})

interface World {
    authId?: string;
    id?: string;
    shareCode?: string;
    world: {
        Name: string;
        CreationTime: string;
        WorldData: any[][]; // n x n tiles
        ResourceData: {
            Money: number;
            Population: number;
            [other: string]: any; // resource model may change before final deadline.
        }; // resource data
        IsTutorialFinished: boolean;
    }
}


interface DBWorld {
    authId?: string;
    id?: string;
    shareCode?: string;
    world: {
        Name: string;
        CreationTime: string;
        WorldData: {
            [ind: string]: any[]
        }; // n x n tiles
        ResourceData: {
            Money: number;
            Population: number;
            [other: string]: any; // resource model may change before final deadline.
        }; // resource data
        IsTutorialFinished: boolean;
    }
}

app.post("/worlds",  async (req: any, res: any) => {
    const worldData: World = req.body;

    console.log('get')
    if(worldData.id || worldData.shareCode || worldData.authId || !worldData.world) {
        return res.status(400).send("Invalid New World Format");
    }

    worldData.authId = req['authId'];

    const dbWorldMap: {[ind:string]: any[]} = {};
     worldData.world.WorldData.map((val, ind) => {
         dbWorldMap[ind.toString()] = val;
     });

    let dbWorld: DBWorld = {
        ...worldData,
        world: {
            ...worldData.world,
            WorldData: dbWorldMap
        }
    };


    try {
        const newWorld = await worldcollection.add(dbWorld);

        res.status(201).send(newWorld.id);
    } catch (err) {
        console.log(err)
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
        const dbWorldMap: {[ind:string]: any[]} = {};
        updatedWorld.world.WorldData.map((val, ind) => {
            dbWorldMap[ind.toString()] = val;
        });

        let dbWorld = {
                ...worldData.world,
                WorldData: dbWorldMap
            }

        await worldDoc.update({
            world: dbWorld
        })

        return res.status(204).send();

    } catch (err) {
        console.log(err);
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

app.post("/worlds/:id/sharecode", async (req:any ,res:any) => {
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

        await worldDoc.update({
            shareCode: newShareCode
        });


        return res.status(200).send(newShareCode);
    }



})

app.delete("/worlds/:id/sharecode", async (req:any ,res:any) => {
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
        console.log('try to share');

        await worldDoc.update({
            shareCode: firebase.firestore.FieldValue.delete()
        });

    return res.status(204).send();


})


async function generateUniqueShareCode(): Promise<string> {

    const shareCode  = crypto.randomBytes(3).toString('hex');

    const potentialExisting = await worldcollection.where("shareCode","==", shareCode).get();

    if (!potentialExisting.empty) { // re-generate if id exists in db.
        return generateUniqueShareCode();
    }

    return shareCode;

}


app.get("/sharedworlds/:shareCode", async (req: any, res: any) => {

    const potentialExisting = await worldcollection.where("shareCode","==", req.params['shareCode']).get();
    console.log(req.params['shareCode'])

    if (potentialExisting.size == 0) {
        return res.status(404).send();
    }
    const worldToSend = potentialExisting.docs[0].data();
    res.status(200).send(worldToSend); // assume only one doc exists
})


// I have learned that firebase does not support cloud functions in sydney yet, oh well.
exports.api = functions.region(SUPPORTED_REGIONS[2]).https.onRequest(app);