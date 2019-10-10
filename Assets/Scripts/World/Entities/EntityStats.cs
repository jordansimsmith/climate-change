namespace World.Entities {
    [System.Serializable]
    public struct EntityStats {
        public int cost;
        public int money;
        public int food;
        public int shelter;
        public int power;
        public int environment;
    }

    public struct EntityStatsTuple  {
        public EntityStats demand;
        public EntityStats supply;
    }
}