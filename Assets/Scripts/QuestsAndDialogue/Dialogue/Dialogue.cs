using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;


    /*
    Baker:
    Hello,
    Can you help me finding some wheat for me?
    I'll give you a reward for it.


    Blacksmith:
    Greetings,
    I'm in need of some iron ore.
    There should be an iron ore deposit in the wild.
    Can you get some for me?
    Thanks in advance. 

    ???

    */

}
