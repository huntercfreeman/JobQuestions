using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toolbelt.Blazor.SpeechSynthesis;

namespace Magic8Ball.Components
{
    public partial class Magic8BallRenderer : ComponentBase
    {
        [Inject]
        public SpeechSynthesis SpeechSynthesis { get; set; }

        private readonly Random rnd = new Random();

        private string UserQuestion { get; set; }
        private List<string> Responses { get; set; } = new List<string>();
        private string SelectedResponse { get; set; } = String.Empty;
        private int LoadingPosition { get; set; } = 250;
        private int MarginNumber { get; set; }
        private bool IsLoading { get; set; }
        private CancellationTokenSource CancellationTokenSource { get; set; }

        private string DisplayResponse 
        { 
            get
            {
                if(string.IsNullOrEmpty(UserQuestion))
                {
                    return $"<text x='50%' y='47%' text-anchor='middle' fill='white' stroke='black' stroke-width='.5px' dy='.3em' font-size='18'>Ask</text>" +
                        $"<text x='50%' y='50%' text-anchor='middle' fill='white' stroke='black' stroke-width='.5px' dy='.3em' font-size='18'>Something!</text>";
                }
                else if(SelectedResponse.Length < 15)
                {
                    return $"<text x='50%' y='50%' text-anchor='middle' fill='white' stroke='black' stroke-width='.5px' dy='.3em' font-size='18'>{SelectedResponse}</text>";
                }
                else
                {
                    return $"<text x='50%' y='47%' text-anchor='middle' fill='white' stroke='black' stroke-width='.5px' dy='.3em' font-size='18'>{SelectedResponse.Substring(0, 15)}</text>" +
                        $"<text x='50%' y='50%' text-anchor='middle' fill='white' stroke='black' stroke-width='.5px' dy='.3em' font-size='18'>{SelectedResponse.Substring(15)}</text>";
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            AddDefaultResponses();
        }

        private async void AskQuestion()
        {
            if (string.IsNullOrEmpty(UserQuestion))
            {
                return;
            }
            
            if(this.CancellationTokenSource != null) this.CancellationTokenSource.Cancel();

            this.CancellationTokenSource = new CancellationTokenSource();

            LoadingPosition = 250;
            IsLoading = true;
            StateHasChanged();
            await Animate(this.CancellationTokenSource.Token).ContinueWith(async task => 
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    int index = rnd.Next(0, Responses.Count);

                    SelectedResponse = Responses[index];

                    IsLoading = false;
                    await InvokeAsync(StateHasChanged);

                    SpeechSynthesis.Speak(SelectedResponse);
                }
                else
                {
                    Console.WriteLine("Faulted");
                }
            });
        }

        private void AddResponseWithWeight(string response, int ticketCount)
        {
            for(int i = 0; i < ticketCount; i++)
            {
                Responses.Add(response);
            }
        }

        private void AddDefaultResponses()
        {
            AddResponseWithWeight("It is certain.", 1);
            AddResponseWithWeight("It is decidedly so.", 1);
            AddResponseWithWeight("Without a doubt.", 1);
            AddResponseWithWeight("Yes - definitely.", 1);
            AddResponseWithWeight("You may rely on it.", 1);
            AddResponseWithWeight("As I see it, yes.", 1);
            AddResponseWithWeight("Most likely.", 1);
            AddResponseWithWeight("Outlook good.", 1);
            AddResponseWithWeight("Yes.", 1);
            AddResponseWithWeight("Signs point to yes.", 1);
            AddResponseWithWeight("Reply hazy, try again.", 1);
            AddResponseWithWeight("Ask again later.", 1);
            AddResponseWithWeight("Better not tell you now.", 1);
            AddResponseWithWeight("Cannot predict now.", 1);
            AddResponseWithWeight("Concentrate and ask again.", 1);
            AddResponseWithWeight("Don't count on it.", 1);
            AddResponseWithWeight("My reply is no.", 1);
            AddResponseWithWeight("My sources say no.", 1);
            AddResponseWithWeight("Outlook not so good.", 1);
            AddResponseWithWeight("Very doubtful.", 1);
        }

        private async Task Animate(CancellationToken cancellationToken)
        {
            try
            {
                for(int i =260; i<=350; i += 10)
                {
                    await Task.Delay(100, cancellationToken);
                    MarginNumber = MarginNumber == 10 ? 0 : 10;
                    LoadingPosition = i;
                    StateHasChanged();
                }  
            }
            catch(TaskCanceledException)
            {
                Console.WriteLine("Task Canceled");
                throw;
            }
        }
    }
}
