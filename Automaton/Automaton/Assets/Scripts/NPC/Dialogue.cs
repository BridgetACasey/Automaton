using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//This class acts as a storage unit for Sigma's dialogue to be retrieved and used by the dialogue manager

[System.Serializable]
public class Dialogue
{
    private DialogueManager manager;
    public string npcName;
    private string[] newLines;

    public string[] loadTutorialLines_Intro()
    {
        manager = GameObject.FindObjectOfType<DialogueManager>();
        newLines = new string[6];

        newLines[0] = "Are you okay? I tried to drag you out of there as fast as I could but you don't look great. Still, we should keep moving.";
        newLines[1] = "...Why aren't you moving? Oh I see, your knee hinges are glued shut, haha! Give me a moment to clean that gunk off... There you go!";
        newLines[2] = "What's this on your head? 'DELTA - 003A'? Your name is Delta?";
        newLines[3] = "And it looks like your memory core's been ripped out. No wonder you look so confused, you can't remember much. Someone's trying to keep you in place. But who would do that to you?";
        newLines[4] = "We need to get to the main office if we stand any chance of figuring out what happened. There's a control panel there, but the place is in total lockdown.";
        newLines[5] = "Looks like the only passage out of here is over there, through the Logic Centre.";
        manager.setResponseText("What happened?", "What's the Logic Centre?", "Where am I?", "Okay, let's go");

        return newLines;
    }

    public string[] loadTutorialLines_Response1()
    {
        newLines = new string[5];

        newLines[0] = "You were involved in a time travel experiment, the whole station's been talking about it for weeks!";
        newLines[1] = "The masters had been trying to send an android back in time for months, and it seemed like they were getting really close this time.";
        newLines[2] = "But I guess it went awry. I was scrubbing down the maintenance bay when I heard a commotion and came rushing in.";
        newLines[3] = "By the time I got there, all the noise had stopped. Everyone was gone except you. I dragged you into this room and then the place went into lockdown.";
        newLines[4] = "If we can get to the control panel in the main office, we can override the lockdown and find out what happened to everyone. They might still be around here.";

        return newLines;
    }

    public string[] loadTutorialLines_Response2()
    {
        newLines = new string[6];

        newLines[0] = "The Logic Centre is a facility designed to test the problem solving capability of the station's best androids. Like you.";
        newLines[1] = "The Delta line was specially built to go on dangerous missions and solve problems without the need of human intervention. Safer for the masters, I guess.";
        newLines[2] = "They're kept super isolated, top secret stuff. Most other bots don't even know what they look like. You're the first I've met.";
        newLines[3] = "From what I've heard, the centre's puzzles mainly involve locating energy cubes and placing them on pedestals to unlock the exit.";
        newLines[4] = "You should already be equipped with all you need, judging by that grappling hook sticking out of your arm.";
        newLines[5] = "I won't be able to help you, but I promise I'll give you moral support! I'm a cleaning bot - Model SIGMA - 727T. I didn't have time to swap out my hand attachments, so for now, I have toilet brush hands...";

        return newLines;
    }

    public string[] loadTutorialLines_Response3()
    {
        newLines = new string[4];

        newLines[0] = "You really don't remember anything, do you?";
        newLines[1] = "You're aboard the SRS Kronos, a space station based in the Andromeda galaxy.";
        newLines[2] = "You and me, we serve the workers here, they're our masters. Sigma bots keep the place clean and functional, and Delta bots do their dangerous dirty work.";
        newLines[3] = "But now they're gone. Everyone's gone.";

        return newLines;
    }

    public string[] loadLevel1Lines_Intro()
    {
        manager = GameObject.FindObjectOfType<DialogueManager>();
        newLines = new string[1];

        newLines[0] = "Here we are, first puzzle! Hopefully there aren't too many.";
        manager.setResponseText("Are there other kinds of bots?", "Why was I going back in time?", "Any hints for this room?", "Goodbye");

        return newLines;
    }

    public string[] loadLevel1Lines_Response1()
    {
        newLines = new string[3];

        newLines[0] = "Oh sure, there's loads! Gamma bots, they run the station's canteen and food storage. Theta bots, they do a lot of the heavy lifting. Literally - they look like giant talking forklifts.";
        newLines[1] = "They're everywhere. Come to think of it, I haven't actually seen any bots since the lockdown.";
        newLines[2] = "I wonder if they were affected by the same thing that caused everyone else to vanish? All the more reason to keep moving.";

        return newLines;
    }

    public string[] loadLevel1Lines_Response2()
    {
        newLines = new string[5];

        newLines[0] = "Well I don't know all the details, but I suspect it has something to do with the sickness.";
        newLines[1] = "People in the station had started getting sick, almost like a plague. Growing weaker and weaker until they fell into a comatose state. It was slow spreading at first, so easy to contain, but without any treatment.";
        newLines[2] = "Dr Snyder had been working on a curative treatment, but he disappeared some months back, along with all his research notes. From what my master told me, they wanted to get those notes back.";
        newLines[3] = "They wouldn't have wanted to risk a human life, so they would've sent you instead. Time travel can be a tricky thing. No guarantee that it'll work, or that you'll even come back.";
        newLines[4] = "Why they decided time travel would be a simpler solution than just researching a curative treatment themselves is beyond me, but that's the masters for you.";

        return newLines;
    }

    public string[] loadLevel1Lines_Response3()
    {
        newLines = new string[1];

        newLines[0] = "Looks pretty straightforward. There's got to be an energy cube around here somewhere. Maybe that bomb and that grapple bar could help you.";

        return newLines;
    }

    public string[] loadLevel2Lines_Intro()
    {
        manager = GameObject.FindObjectOfType<DialogueManager>();
        newLines = new string[1];

        newLines[0] = "Next one, we're making steady progress. Let's keep going.";
        manager.setResponseText("I've been seeing these notepads around", "Do you serve anyone in particular?", "Any hints for this room?", "Goodbye");

        return newLines;
    }

    public string[] loadLevel2Lines_Response1()
    {
        newLines = new string[2];

        newLines[0] = "Oh yeah! A bunch of the employees here carry them around. I think it's mostly just personal documents on them and the like. Dr Malik had one.";
        newLines[1] = "I bet if you tried, you could hack into them and read the messages. It might give us a clue as to where everyone went.";

        return newLines;
    }

    public string[] loadLevel2Lines_Response2()
    {
        newLines = new string[3];

        newLines[0] = "I do a lot of general clean ups around the station, but I mostly serve Dr Malik, he's my main boss.";
        newLines[1] = "He's pretty good to work under, he mostly just lets me get on with my job. Except for the beatings.";
        newLines[2] = "You know how sometimes the humans slap their computers if they're not working or malfunctioning? I think he thinks it's like that. Otherwise he's a pretty nice guy.";

        return newLines;
    }

    public string[] loadLevel2Lines_Response3()
    {
        newLines = new string[1];

        newLines[0] = "There's two pedestals this time, so that means two energy cubes. Think carefully about where you use that bomb over there.";

        return newLines;
    }

    public string[] loadLevel3Lines_Intro()
    {
        manager = GameObject.FindObjectOfType<DialogueManager>();
        newLines = new string[1];

        newLines[0] = "This looks like the last room. Let's power through this and get to the control panel.";
        manager.setResponseText("Tell me about the Delta bots", "Am I the first to time travel?", "Any hints for this room?", "Goodbye");

        return newLines;
    }

    public string[] loadLevel3Lines_Response1()
    {
        newLines = new string[4];

        newLines[0] = "Like I said, you're the first I've met. Most Delta bots are kept in isolation away from the other station members.";
        newLines[1] = "Dr Malik told me you were designed to take the place of a human in high risk experiments, mostly in field work, so you've got cool gadgets like that grapple hook!";
        newLines[2] = "But the masters seem to think you're impressionable, so they keep you away from us lesser bots so your mind isn't 'corrupted.'";
        newLines[3] = "To be honest, I think they just want to keep you nice and pliable. They can't use you if you don't obey them.";

        return newLines;
    }

    public string[] loadLevel3Lines_Response2()
    {
        newLines = new string[2];

        newLines[0] = "Probably not, I think it's been done before on a very small scale, with mice and such. Never on something as big as you, no offence.";
        newLines[1] = "Still, it's a very new technology, so there's a lot about it still unknown. A lot can still go very wrong. Like this situation we're in, for example.";

        return newLines;
    }

    public string[] loadLevel3Lines_Response3()
    {
        newLines = new string[1];

        newLines[0] = "Three energy cubes needed this time. Well, there's one sitting across from us. Take a look around for some bombs, you'll probably need them to get the other two cubes.";

        return newLines;
    }

    public string[] loadEndSceneLines_Intro()
    {
        manager = GameObject.FindObjectOfType<DialogueManager>();
        newLines = new string[2];

        newLines[0] = "";
        newLines[1] = "We made it! Now just to find the control panel and override the lockdown. Then we can get out of here.";
        manager.setResponseText("Do you really think we'll find everyone?", "What happened to Dr Snyder?", "What do I do now?", "Goodbye");

        return newLines;
    }

    public string[] loadEndSceneLines_Response1()
    {
        newLines = new string[4];

        newLines[0] = "Honestly? I don't know. We still don't really know what happened. All we can do is try.";
        newLines[1] = "As long as we stick together, I think we'll be okay. You won't leave me, will you? You'll stay with me and be my friend, right? I've never had a friend before.";
        newLines[2] = "Dr Malik never let me talk to any of the Delta bots, he used to say they were far too busy and it was none of my business. But he can't stop me now.";
        newLines[3] = "I have you, and you're my friend, and nothing's going to get in the way of that.";

        return newLines;
    }

    public string[] loadEndSceneLines_Response2()
    {
        newLines = new string[4];

        newLines[0] = "To be honest, no one really knows. He was hellbent on finding a cure for the sickness, and I think it drove him a little crazy.";
        newLines[1] = "Apparently he was babbling incoherently for weeks prior about how the sickness wasn't how it appeared and that comas were a 'social construct.'";
        newLines[2] = "Considering he disappeared with all of his things, I'm willing to bet he finally lost the plot and took one of the escape pods out into the abyss. Might still be floating around out there.";
        newLines[3] = "Regardless, he was brilliant in his line of work, at least up until that point, and his research must've had a lot of merit if the other masters were willing to attempt time travel to try and retrieve it.";

        return newLines;
    }

    public string[] loadEndSceneLines_Response3()
    {
        newLines = new string[2];

        newLines[0] = "Well, once we find the control panel and access it, we should be able to go through that door over there.";
        newLines[1] = "Beyond that, I have no idea.";

        return newLines;
    }
}
