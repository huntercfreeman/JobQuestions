using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbelt.Blazor.SpeechSynthesis;

namespace Magic8Ball.Components
{
    public partial class Magic8BallRenderer : ComponentBase
    {
        [Inject]
        public SpeechSynthesis SpeechSynthesis { get; set; }

        private readonly Random rng = new Random();

        public string UserQuestion { get; set; }
        public List<string> Responses { get; set; } = new List<string>();
        public string SelectedResponse { get; set; } = "";
        public int LoadingPosition { get; set; } = 250;
        public int AnimationNumber { get; set; }
        public int MarginNumber { get; set; }
        public bool IsLoading { get; set; }

        public string DisplayResponse 
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

        public async void AskQuestion()
        {
            if (string.IsNullOrEmpty(UserQuestion))
            {
                return;
            }
            
            int animationNumber = ++AnimationNumber;
            
            LoadingPosition = 250;
            IsLoading = true;
            StateHasChanged();
            await Animate(animationNumber);
            if (animationNumber != AnimationNumber)
            {
                return;
            }

            int index = rng.Next(0, Responses.Count);

            SelectedResponse = Responses[index];

            IsLoading = false;
            StateHasChanged();

            SpeechSynthesis.Speak(SelectedResponse);
        }

        public void AddResponseWithWeight(string response, int ticketCount)
        {
            for(int i = 0; i < ticketCount; i++)
            {
                Responses.Add(response);
            }
        }

        public void AddDefaultResponses()
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

        public async Task Animate(int animationNumber)
        {
            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 10;
            LoadingPosition = 260;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 0;
            LoadingPosition = 270;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 10;
            LoadingPosition = 280;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 0;
            LoadingPosition = 290;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 10;
            LoadingPosition = 300;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 0;
            LoadingPosition = 310;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 10;
            LoadingPosition = 320;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 0;
            LoadingPosition = 330;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 10;
            LoadingPosition = 340;
            StateHasChanged();

            await Task.Delay(100);
            if (animationNumber != AnimationNumber)
            {
                return;
            }
            MarginNumber = 0;
            LoadingPosition = 350;
            StateHasChanged();
        }
    }
}
