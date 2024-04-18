using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    const int MAX_LEAF_SIZE = 20;
    Stack<Leaf> leafs = new Stack<Leaf>();

    Leaf leaf;
    private void Start()
    {

        Texture2D sprMap = new Texture2D(500, 500);

        Random r = new Random();
        Leaf root = new Leaf(0, 0, sprMap.width, sprMap.height);
        leafs.Push(root);

        bool did_split = true;

        while (did_split)
        {
            did_split = false;
            foreach (Leaf l in leafs)
            {
                if (l.leftChild == null && l.rightChild == null)
                {
                    // if this Leaf is not already split...		
                    // if this Leaf is too big, or 75% chance...			
                    if (l.width > MAX_LEAF_SIZE || l.height > MAX_LEAF_SIZE || r.NextDouble() > 0.25)
                    {
                        if (l.Split())
                        // split the Leaf!				
                        {
                            // if we did split, push the child leafs to the Vector so we can loop into them next					
                            leafs.Push(l.leftChild);
                            leafs.Push(l.rightChild);
                            did_split = true;
                            Debug.Log("Did Split: " + l.leftChild + ", " + l.rightChild);
                        }
                    }
                }
            }
        }
    }


}
