import {firebaseApp} from "./firebase";


const getAuthToken = (req: any, res: any, next: any) => {
    if (
        req.headers.authorization &&
        req.headers.authorization.split(' ')[0] === 'Bearer'
    ) {
        req.authToken = req.headers.authorization.split(' ')[1];
    } else {
        req.authToken = null;
    }
    next();
};

export const checkIfAuthenticated = (req: any, res: any, next: any) => {
    getAuthToken(req, res, async () => {
        try {
            const { authToken } = req;
            const userInfo = await firebaseApp
                .auth()
                .verifyIdToken(authToken);
            req.authId = userInfo.uid;
            return next();
        } catch (e) {
            console.log(e.message);
            return res
                .status(401)
                .send({ error: 'You are not authorized to make this request ' +req.authToken });
        }
    });
};
