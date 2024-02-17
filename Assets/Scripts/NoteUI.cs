using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : Singleton<NoteUI>
{
    [SerializeField] private GameObject content;
    [SerializeField] private List<GameObject> notes;
    private int _currentNote;

    protected override void Awake()
    {
        base.Awake();
        _currentNote = 0;
    }

    private void Update()
    {
        for (var i = 0; i < notes.Count; i++)
        {
            if(i == _currentNote)
                notes[i].SetActive(true);
            else
                notes[i].SetActive(false);
        }
    }

    public void Show()
    {
        content.SetActive(true);
    }

    public void Next()
    {
        _currentNote++;
        _currentNote %= GameManager.instance.GetNotes();
    }
    
    public void Previous()
    {
        _currentNote--;
        if (_currentNote < 0)
            _currentNote = GameManager.instance.GetNotes() - 1;
    }
}
