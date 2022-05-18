using System;

namespace metanit
{
    class Message
    {
        public string Text { get; }
        public Message(string text) => Text = text;
        public virtual void Print() => Console.WriteLine($"Message: {Text}");
    }
    class EmailMessage : Message
    {
        public EmailMessage(string text) : base(text) { }
        public override void Print() => Console.WriteLine($"Email: {Text}");
    }
    class SmsMessage : Message
    {
        public SmsMessage(string text) : base(text) { }
        public override void Print() => Console.WriteLine($"Sms: {Text}");
    }
    delegate Message MessageBuilder(string text);

    delegate T MessageBuilder<out T>(string text);
    delegate void MessageReceiver<in T>(T message);
    class Program
    {
         static EmailMessage WriteEmailMessage(string text) => new EmailMessage(text);
        static void ReceiveMessage(Message message) => message.Print();
        static void Main(string[] args)
        {
            //MessageBuilder<Message> messageBuilder = WriteEmailMessage;     // ковариантность
            //Message message = messageBuilder("hello Tom"); // вызов делегата
            //message.Print(); // Email: hello Tom

            MessageBuilder<EmailMessage> messageBuilder = WriteEmailMessage;     // ковариантность
            MessageBuilder<Message> message = messageBuilder; // вызов делегата
            message("hello Tom").Print(); // Email: hello Tom


            //MessageReceiver<EmailMessage> messageReceiver = ReceiveMessage; // контравариантность
            //messageReceiver(new EmailMessage("Hello World!"));

            MessageReceiver<Message> messageReceiver = ReceiveMessage; // контравариантность
            MessageReceiver<EmailMessage> message2 = messageReceiver;
            message2(new EmailMessage("Hello World!"));
        }
    }
}
