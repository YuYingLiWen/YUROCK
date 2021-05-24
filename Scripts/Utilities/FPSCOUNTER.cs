
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class FPSCOUNTER : MonoBehaviour
{
    Text t;
    Queue queue = new Queue();
    private void Awake()
    {
        t = GetComponent<Text>();
    }

    void Update()
    {
        
        int fps = (int)(1 / Time.deltaTime);
        int sum = 0;

        if(queue.Count >= 60) queue.Dequeue();

        queue.Enqueue(fps);

        foreach(int val in queue) sum += val;

        t.text = $"{fps} FPS\nAvg: {sum/queue.Count} FPS";
    }
}
