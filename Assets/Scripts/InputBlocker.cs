using UnityEngine.EventSystems;
using TMPro;

/*
 * Generated using Claude AI
 * 
 * Reason: Was searching for a way to stop character input while input field is selected
 * and Claude gave me the code for an input blocker that I most likely would not have
 * figured out on my own
 * 
 * Prompt: I currently have a TMPro input field in my game,
 * that allows a player to type something in. But I also have a keybind (C)
 * that quits the user from the screen with the input field. How do I make it so that
 * while the user is typing in the input field, there game doesn't sense any keyboard
 * input other than for typing
 */
public static class InputBlocker
{
    public static bool IsTyping
    {
        get
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            return selected != null && selected.GetComponent<TMP_InputField>() != null;
        }
    }
}