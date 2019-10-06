namespace World.Resource {
    public class ResourceEvent {
        private readonly bool exceeded;
        private readonly int threshold;
        public bool WentAbove => exceeded == true;
        public bool WentBelow => exceeded == false;
        public int Threshold => threshold;

        public ResourceEvent(bool exceeded, int threshold) {
            this.exceeded = exceeded;
            this.threshold = threshold;
        }
    }
}