using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Dialogue_Manager : MonoBehaviour
{
    public TextMeshProUGUI Dialouge_Text, Character_Text;
    public GameObject Dialouge_Object;
    public string interact_key;
    public NPCObject NPC;
    public UnityEvent OnInteract, OnFinish;
    public BoolData ConvStart;
    public List<UnityEvent> dialogueActions;

    private bool inRange;
    private string _text_to_display;
    private float textScrollSpeed;
    private string _actionCharacter = "^";
    private int _actionIndex;
    
    private void Start()
    {
        inRange = false;
        ConvStart.value = false;
        Dialouge_Text.text = "";
        Character_Text.text = "";
        Dialouge_Object.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void FixedUpdate()
    {
        if (inRange && !(ConvStart.value) && Input.GetButtonDown(interact_key))
        {
            OnInteract.Invoke();
        }
    }

    public void StartConv()
    {
        if (!ConvStart.value){
            ConvStart.value = true;
            Dialouge_Object.SetActive(true);
            StartCoroutine(ScrollText());
        }
    }
    
    public IEnumerator ScrollText()
    {
        Character_Text.text = NPC.dialogue.characterName;
        for (int i = 0; i < NPC.dialogue.lines.Count; i++)
        {
            _text_to_display = "";
            if (NPC.dialogue.lines[i].Contains(_actionCharacter))
            {
                Debug.Log("Action: ");
                
                _actionIndex = int.Parse(NPC.dialogue.lines[i].Split('^')[1]);
                dialogueActions[_actionIndex].Invoke();
            }
            else
            {
                textScrollSpeed = .001f;
                for (int j = 0; j < NPC.dialogue.lines[i].Length; j++)
                {
                    _text_to_display += NPC.dialogue.lines[i][j];
                    Dialouge_Text.text = _text_to_display;
                    yield return new WaitForSeconds(textScrollSpeed);
                    if (Input.GetButtonDown(interact_key))
                    {
                        Dialouge_Text.text = NPC.dialogue.lines[i];
                        break;
                    }
                }

                yield return new WaitForSeconds(.01f);
                yield return new WaitUntil(() => Input.GetButtonDown(interact_key));
            }
        }
        
        Dialouge_Object.SetActive(false);
        OnFinish.Invoke();
        ConvStart.value = false;
    }

    public void CloseDialogue()
    {
        Dialouge_Object.SetActive(false);
        ConvStart.value = false;
    }


}
