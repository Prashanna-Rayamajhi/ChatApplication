using ChatAppClient.Utilities;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatAppClient.ViewModel
{
    ///<Summary>
    ///This code is copied from link: https://github.com/mindofai/Build2019Chat/blob/master/BuildChat/BuildChat/ViewModels/MainViewModel.cs
    ///This code is being used to address new ChatApp page using view model Chatview model and register it to hub
    ///</Summary>
    class ChatViewModel : INotifyPropertyChanged
    {
       
            public event PropertyChangedEventHandler PropertyChanged;

            private string _name;
            private string _message;
            private ObservableCollection<MessageModel> _messages;
            private bool _isConnected;
       

            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }


            public string Message
            {
                get
                {
                    return _message;
                }
                set
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }

            public ObservableCollection<MessageModel> Messages
            {
                get
                {
                    return _messages;
                }
                set
                {
                    _messages = value;
                    OnPropertyChanged();
                }
            }
            public bool IsConnected
            {
                get
                {
                    return _isConnected;
                }
                set
                {
                    _isConnected = value;
                    OnPropertyChanged();
                }
            }

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }

        public ChatViewModel(string name)
        {
            _name = name;
            Messages = new ObservableCollection<MessageModel>();
                
            ConnectCommand = new Command(async () => await Connect());
            SendMessageCommand = new Command(async () => { await SendMessage(Name, Message); });
            DisconnectCommand = new Command(async () => await Disconnect());

            IsConnected = false;
        
          
            Jeeves.hubConnection.On<string>("LeaveChat", (user) =>
            {
                    Messages.Add(new MessageModel() { User = Name, Message = $"{user} has left the chat", IsSystemMessage = true });
            });

            Jeeves.hubConnection.On<string, string>("MessageRecieved", (user, message) =>
            {
                    Messages.Add(new MessageModel() { User = user, Message = message, IsSystemMessage = false, IsOwnMessage = Name == user });
            });

            Jeeves.hubConnection.On<string>("JoinChat", (user) =>
            {
                Messages.Add(new MessageModel() { User = Name, Message = $"{user} has joined the chat", IsSystemMessage = true });
            });

        }
       


            async Task Connect()
            {
                await Jeeves.hubConnection.StartAsync();
                await Jeeves.hubConnection.InvokeAsync("JoinChat", Name);

                IsConnected = true;
            }

             async Task SendMessage(string user, string message)
            {
                await Jeeves.hubConnection.InvokeAsync("SendMessage", user, message);
            }

            async Task Disconnect()
            {
                await Jeeves.hubConnection.InvokeAsync("LeaveChat", Name);
                await Jeeves.hubConnection.StopAsync();

                IsConnected = false;
                
            }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        
    }
}
