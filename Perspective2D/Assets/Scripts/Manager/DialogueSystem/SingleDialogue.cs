using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SingleDialogue", order = 1)]
public class SingleDialogue : ScriptableObject
{

    public Sprite characterASprite;
    public Sprite characterBSprite;

    public List<Sentence> conversations;

}

[Serializable]
public class Sentence
{
    public string words;
    public Roles owners;
}

public enum Roles
{
    RoleA, RoleB
}