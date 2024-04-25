using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int maxLeafSize = 100;
    [SerializeField] float InvertedSplitChance = 0.25f;
    [SerializeField] GameObject plane = null;

    Color[] colors = { Color.white };
    Stack<Leaf> leaves = new Stack<Leaf>();
    Leaf leaf;

    Texture2D tex;

    private void Start()
    {
        SetTexture();
        SplitLeaves();
    }

    private void SetTexture()
    {
        plane.GetComponent<Renderer>().material.mainTexture = tex;

        

        tex = new Texture2D(500, 500);
        for (int x = 10; x > 110; x++)
        {
            for (int y = 10; y > 110; y++)
            {
                tex.SetPixel(x, y, Color.green);
            }
        }
        tex.Apply();

      
        
    }

    private void SplitLeaves()
    {
        Leaf root = new Leaf(0, 0, tex.width, tex.height);
        leaves.Push(root);

        bool did_split = true;

        while (did_split)
        {
            did_split = false;

            // We can't modify the list we are iterating over so we make a copy.
            List<Leaf> tempLeaves = new List<Leaf>(leaves);
            foreach (Leaf l in tempLeaves)
            {
                if (l.leftChild == null && l.rightChild == null)
                {
                    // if this Leaf is not already split...		
                    // if this Leaf is too big, or 75% chance...			
                    if (l.width > maxLeafSize || l.height > maxLeafSize || Random.Range(0f,1f) > InvertedSplitChance)
                    {
                        if (l.Split())
                        // split the Leaf!				
                        {
                            //Debug.Log("x: " + l.leftChild.x + " y: " + l.leftChild.y + " w: " + l.leftChild.width + " h: " + l.leftChild.height);
                            
                            //tex.SetPixels(l.leftChild.x, l.leftChild.y, l.leftChild.width -1, l.leftChild.height -1, colors);
                            // if we did split, push the child leafs to the stack so we can loop into them next					
                            leaves.Push(l.leftChild);
                            leaves.Push(l.rightChild);
                            did_split = true;
                        }
                    }
                }
            }
        }
    }
 }

