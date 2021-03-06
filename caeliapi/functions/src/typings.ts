
export interface World {
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
            [other: string]: any; // resource model may change
        }; // resource data
        IsTutorialFinished: boolean;
    }
}

// Kept seperate on purpose, no need for inheritance right now.
export interface DBWorld {
    authId?: string;
    id?: string;
    shareCode?: string;
    world: {
        Name: string;
        CreationTime: string;
        WorldData: {
            [ind: string]: any[] // firestore friendly format (firestore does not support 2d arrays in oct 2019)
        }; // n x n tiles
        ResourceData: {
            Money: number;
            Population: number;
            [other: string]: any; // resource model may change
        }; // resource data
        IsTutorialFinished: boolean;
    }
}