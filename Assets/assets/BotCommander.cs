using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCommander : MonoBehaviour
{
    private List<string> instrucciones = new List<string>();
    private bool waitingInstruction = false;
    private bool runingInstructions = true;
    private int currInstIndex = 0;


    private void Start()
    {
        ListaInstrucciones();
    }

    private void Instruccion(string instruccion)
    {
        instrucciones.Add(instruccion);
    }

    /// <summary>
    /// Gira en la direccion indicada
    /// </summary>
    /// <param name="direccion">"izq" gira a la izquierda, "der" gira a la derecha</param>
    protected void Girar(string direccion)
    {
        switch (direccion)
        {
            case "izq":
                Instruccion("girar izq");
                break;
            case "der":
                Instruccion("girar der");
                break;
            default:
                Instruccion("girar "+direccion);
                break;
        }
    }
    /// <summary>
    /// Avanza una cantidad de pasos determinados
    /// </summary>
    /// <param name="pasos">numero de pasos</param>
    protected void Avanzar(int pasos)
    {
        Instruccion("avanzar "+pasos);
    }

    protected virtual void ListaInstrucciones()
    {
        
    }


    private void Update()
    {
        if (runingInstructions)
        {
            if (!waitingInstruction)
            {
                if (currInstIndex >= instrucciones.Count)
                {
                    runingInstructions = false;
                }
                else
                {
                    waitingInstruction = true;
                    RunInstruction(instrucciones[currInstIndex],currInstIndex);
                    currInstIndex++;
                }
            }
        }
    }

    private void RunInstruction(string instruction, int index)
    {
        var instArray = instruction.Split(" ");
        if (string.IsNullOrEmpty(instArray[0]))
        {
            ShowError(index, instruction);
        }

        string inst = instArray[0];
        switch (inst)
        {
            case "avanzar":
                int steps = 0;
                if (!string.IsNullOrEmpty(instArray[1]))
                {
                    if (int.TryParse(instArray[1], out steps))
                    {
                        if (steps >= 1)
                        {
                            ShowInstruction(index, instruction);
                            StartCoroutine(AvanzarCoroutine(steps));
                            return;
                        }
                    }
                }
                ShowError(index, instruction);
                return;
            case "girar":
                if (!string.IsNullOrEmpty(instArray[1]))
                {
                    switch (instArray[1])
                    {
                        case "izq":
                            ShowInstruction(index, instruction);
                            StartCoroutine(GirarCoroutine(0));
                            return;
                        case "der":
                            ShowInstruction(index, instruction);
                            StartCoroutine(GirarCoroutine(1));
                            return;
                    }
                }
                ShowError(index, instruction);
                return;
        }
    }

    private void ShowError(int line, string instruction)
    {
        DebugText.ShowError("No entiendo la instruccion: "+line+" "+ instruction);
        runingInstructions = false;
    }
    
    private void ShowCollitionWall()
    {
        DebugText.ShowError("Perdiste: Pared");
        runingInstructions = false;
    }
    
    private void ShowCollitionGoal()
    {
        DebugText.ShowGoal("GANASTE!");
        runingInstructions = false;
    }
    
    private void ShowInstruction(int line, string instruction)
    {
        DebugText.ShowText("Ejecutando: "+(line+1)+" "+ instruction);
    }
    
    private IEnumerator AvanzarCoroutine(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (IsRodColliding())
            {
                if (RodCollider.collidingTag == "GOAL")
                {
                    ShowCollitionGoal();
                }
                else
                {
                    ShowCollitionWall();
                }
                
                runingInstructions = false;
                yield break;
            }
            Transform transf = transform;
            transf.position = transf.position + transf.forward;
            yield return new WaitForSeconds(1);
        }
        waitingInstruction = false;
        yield return null;
    }
    private IEnumerator GirarCoroutine(int dir)
    {
        if (dir == 0)
        {
            transform.Rotate(new Vector3(1,0,0),-90);
        }
        else
        {
            transform.Rotate(new Vector3(1,0,0),90);
        }
        
        yield return new WaitForSeconds(1);
        waitingInstruction = false;
        yield return null;
    }

    private bool IsRodColliding()
    {
        return RodCollider.isColliding;
    }
}