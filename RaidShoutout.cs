using System;
using System.Collections.Generic;
using Twitch.Common.Models.Api;

public class CPHInline
{
    const string SCENE = "Game Capture";
    const string CLIP_SOURCE = "Browser - Raids";
    const string RAIDER_AVATAR_SORUCE = "Browser - Raider Avatar";
    const string RAIDER_DESCIRPTION = "Text - Raider Description";
    public bool Execute()
    {
        string userId;
        int length;
        int clipNumber;
        DateTime start;
        DateTime end;
        end = DateTime.Now;
        start = end.AddDays(-365);
        //Get the userId and retrieve clip data
        CPH.TryGetArg("userId", out userId);
        List<ClipData> clip = CPH.GetClipsForUserById(userId, start, end);
        TwitchUserInfoEx userInfo = CPH.TwitchGetExtendedUserInfoById(userId);
        //Check if any clips are there
        if (clip.Count > 0)
        {
            //Get a random clip by the number
            clipNumber = CPH.Between(0, clip.Count);
            //Determine the length of the clip, convert to milliseconds
            length = (int)clip[clipNumber].Duration * 1000;
            //Set the Raid Browser Source's URL
            CPH.ObsSetBrowserSource(SCENE, CLIP_SOURCE, clip[clipNumber].Url);
            CPH.ObsShowSource(SCENE, CLIP_SOURCE);
            string imageUrl = userInfo.ProfileImageUrl;
            CPH.ObsSetBrowserSource(SCENE, RAIDER_AVATAR_SORUCE, imageUrl);
            CPH.ObsShowSource(SCENE, RAIDER_AVATAR_SORUCE);
            string description = userInfo.Description;
            CPH.ObsSetGdiText(SCENE, RAIDER_DESCIRPTION, description);
            CPH.ObsShowSource(SCENE, RAIDER_DESCIRPTION);
            //Wait for the clip's duration, then hide the sources
            CPH.Wait(length);
            CPH.ObsHideSource(SCENE, CLIP_SOURCE);
            CPH.ObsHideSource(SCENE, RAIDER_AVATAR_SORUCE);
            CPH.ObsHideSource(SCENE, RAIDER_DESCIRPTION);
        }

        return true;
    }
}
