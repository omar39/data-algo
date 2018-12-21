using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Linq;
using MaterialUI;

public class expand : MonoBehaviour {


    public GameObject start;
    public GameObject bubble;
    public GameObject selection;
    public GameObject insertion;
    public GameObject stop;
    public int size;
    List<int> values;
    List<GameObject> created;
    
    void Start()
    {
        //bubble.backgroundImage.color = Color.grey;
        //selection.backgroundImage.color = Color.grey;
        //insertion.backgroundImage.color = Color.grey;
        created = new List<GameObject>();
        values = new List<int>();
    }

    public void clear()
    {
        for (int i = 0; i < created.Count; i++) Destroy(created[i]);
        values = new List<int>();
        bubble.GetComponent<Image>().color = Color.grey;
        selection.GetComponent<Image>().color = Color.grey;
        insertion.GetComponent<Image>().color = Color.grey;
        StopAllCoroutines();
    }

    public void BubbleSort()
    {
        if (bubble.GetComponent<Image>().color != Color.green && selection.GetComponent<Image>().color != Color.green &&
            insertion.GetComponent<Image>().color != Color.green)
        {
            if (created.Count != 0)
            {
                clear();
            }
            bubble.GetComponent<Image>().color = Color.green;
            StartCoroutine(loop());
            StartCoroutine(Bubble());
        }
    }

    public void SelectionSort()
    {
        if (bubble.GetComponent<Image>().color != Color.green && selection.GetComponent<Image>().color != Color.green &&
            insertion.GetComponent<Image>().color != Color.green)
        {
            if (created.Count != 0)
            {
                clear();
            }
            selection.GetComponent<Image>().color = Color.green;
            StartCoroutine(loop());
            StartCoroutine(Selection());
        }
    }

    public void InsertionSort()
    {
        if (bubble.GetComponent<Image>().color != Color.green && selection.GetComponent<Image>().color != Color.green &&
            insertion.GetComponent<Image>().color != Color.green)
        {
            if (created.Count != 0)
            {
                clear();
            }
            insertion.GetComponent<Image>().color = Color.green;
            StartCoroutine(loop());
            StartCoroutine(Insertion());
        }
    }

    public IEnumerator show()
    {
        for (int i = 3; i >= 1; i--)
        {
            GameObject temp = Instantiate(start, transform.position + new Vector3(30, 10, 0f), transform.rotation);
            temp.GetComponent<Renderer>().material.color = Color.black;
            temp.GetComponentInChildren<TextMeshPro>().text = i.ToString();
            yield return new WaitForSeconds(1);
            Destroy(temp);
        }

    }

    public IEnumerator loop()
    {
        values = new List<int>();
        created = new List<GameObject>();
        for (int i = 0; i < 11; i++)
        {
            int x = Random.Range(1, 29);
            values.Add(x);
        }
        size = values.Count;
        for (int i = 0; i < size; i++)
        {
            int carrier = values[i];
            GameObject current = Instantiate(start, transform.position, transform.rotation);
            current.GetComponentInChildren<TextMeshPro>().text = carrier.ToString();
            //current.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = i.ToString();
            created.Add(current);
            transform.position += new Vector3(6, 0f, 0f);
            yield return null;
        }
        transform.position += new Vector3(-6 * size, 0, 0);
    }

    public IEnumerator Insertion()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(show());
        for (int i = 0; i < size ; i++)
        {
            int inner = i;
            created[inner].GetComponent<Renderer>().material.color = Color.grey; yield return null;
            yield return StartCoroutine(moveVerticalup(inner, created[inner].transform.position));
            yield return new WaitForSeconds(1f);
            while (inner > 0 && values[inner] < values[inner - 1])
            {
                yield return new WaitForSeconds(1f);
                Vector3 temp1 = created[inner].transform.position + new Vector3(-6, 0, 0);
                Vector3 temp2 = created[inner - 1].transform.position + new Vector3(6, 0, 0);
                while (created[inner].transform.position != temp1)
                {
                    created[inner].transform.position = Vector3.MoveTowards(created[inner].transform.position, temp1, 15 * Time.deltaTime);
                    yield return null;
                }
                while (created[inner - 1].transform.position != temp2)
                {
                    created[inner - 1].transform.position = Vector3.MoveTowards(created[inner - 1].transform.position, temp2, 15 * Time.deltaTime);
                    yield return null;
                }
                int temp = values[inner];
                values[inner] = values[inner - 1];
                values[inner - 1] = temp;
                GameObject t = created[inner];
                created[inner] = created[inner - 1];
                created[inner - 1] = t;
                inner--;
            }
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(moveVerticaldown(inner, created[inner].transform.position));
            created[inner].GetComponent<Renderer>().material.color = Color.black; yield return null;
        }
        insertion.GetComponent<Image>().color = Color.grey;
    }

    public IEnumerator Bubble()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(show());
        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; j < size - (i + 1); j++)
            {
                created[j].GetComponent<Renderer>().material.color = Color.grey; yield return null;
                created[j + 1].GetComponent<Renderer>().material.color = Color.grey; yield return null;
                yield return new WaitForSeconds(1f);
                if (values[j] > values[j + 1])
                {
                    yield return StartCoroutine(swap(j, j + 1));
                    Vector3 newpos = created[j].transform.position;
                    StartCoroutine(moveHorizontalright(j, created[j + 1].transform.position + new Vector3(0, 12, 0)));
                    yield return StartCoroutine(moveHorizontalleft(j + 1, newpos + new Vector3(0, -12, 0)));
                    int temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                    GameObject t = created[j + 1];
                    created[j + 1] = created[j];
                    created[j] = t;
                }
                created[j].GetComponent<Renderer>().material.color = Color.black; yield return null;
                created[j + 1].GetComponent<Renderer>().material.color = Color.black; yield return null;
            }
        }
        bubble.GetComponent<Image>().color = Color.grey;
    }

    public IEnumerator Selection()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(show());
        for (int i = 0; i < size - 1; i++)
        {
            created[i].GetComponent<Renderer>().material.color = Color.grey; yield return null;
            int carrier = i + 1;
            for (int j = i + 1; j < size; j++)
            {
                created[carrier].GetComponent<Renderer>().material.color = Color.blue; yield return null;
                created[j].GetComponent<Renderer>().material.color = Color.grey; yield return null;
                yield return new WaitForSeconds(1f);
                if (values[carrier] > values[j])
                {
                    created[carrier].GetComponent<Renderer>().material.color = Color.black; yield return null;
                    carrier = j;
                    created[j].GetComponent<Renderer>().material.color = Color.blue; yield return null;
                }
                else
                {
                    created[j].GetComponent<Renderer>().material.color = Color.black; yield return null;
                }
            }
            if (values[i] > values[carrier])
            {
                yield return StartCoroutine(swap(i, carrier));
                Vector3 newpos = created[i].transform.position;
                StartCoroutine(moveHorizontalright(i, created[carrier].transform.position + new Vector3(0, 12, 0)));
                yield return StartCoroutine(moveHorizontalleft(carrier, newpos + new Vector3(0, -12, 0)));
                int temp = values[i];
                values[i] = values[carrier];
                values[carrier] = temp;
                GameObject X = created[i];
                created[i] = created[carrier];
                created[carrier] = X;
            }
            created[i].GetComponent<Renderer>().material.color = Color.black; yield return null;
            created[carrier].GetComponent<Renderer>().material.color = Color.black; yield return null;
        }
        selection.GetComponent<Image>().color = Color.grey;
    }

    public IEnumerator swap(int index1, int index2)
    {
        StartCoroutine(moveVerticalup(index1, created[index1].transform.position));
        yield return StartCoroutine(moveVerticaldown(index2, created[index2].transform.position));
    }

    public IEnumerator moveVerticalup(int index, Vector3 pos)
    {
        while(created[index].transform.position != pos + new Vector3(0, 6, 0))
        {
            created[index].transform.position = Vector3.MoveTowards(created[index].transform.position, pos + new Vector3(0, 6, 0), 15 * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator moveVerticaldown(int index, Vector3 pos)
    {
        while (created[index].transform.position != pos + new Vector3(0, -6, 0))
        {
            created[index].transform.position = Vector3.MoveTowards(created[index].transform.position, pos + new Vector3(0, -6, 0), 15 * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator moveHorizontalright(int index, Vector3 pos)
    {
        while (created[index].transform.position != pos)
        {
            created[index].transform.position = Vector3.MoveTowards(created[index].transform.position, pos, 15 * Time.deltaTime); 
            yield return null;
        }
        yield return StartCoroutine(moveVerticaldown(index, created[index].transform.position));
    }

    public IEnumerator moveHorizontalleft(int index, Vector3 pos)
    {
        while (created[index].transform.position != pos )
        {
            created[index].transform.position = Vector3.MoveTowards(created[index].transform.position, pos, 15 * Time.deltaTime);   
            yield return null;
        }
        yield return StartCoroutine(moveVerticalup(index, created[index].transform.position));
    }

}
