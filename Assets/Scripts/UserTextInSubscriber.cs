using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class UserTextInSubscriber : UnitySubscriber<MessageTypes.Std.String>
    {
        // public Transform PublishedTransform;

        public static string text;
        public bool isMessageReceived;

        protected override void Start()
        {
			base.Start();
		}
		
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Std.String message)
        {
            text = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {}
    }
}