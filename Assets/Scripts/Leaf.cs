using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Drawing;
using System.Numerics;
using System;
using UnityEngine;
using Random = System.Random;

public class Leaf 
{
    const int MIN_LEAF_SIZE = 6;
    public int y, x, width, height;
    public Leaf leftChild;
    public Leaf rightChild;

    public Rectangle room;

    public Leaf(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width; 
        this.height = height;
    }

    public bool Split()
    {
        if(leftChild != null || rightChild != null)
            return false;


        // if the width is >25% larger than height, we split vertically		
        // if the height is >25% larger than the width, we split horizontally		
        // otherwise we split randomly		
        Random r = new Random();
        var splitH = r.NextDouble() > 0.5;
        
        if (width > height && width / height >= 1.25)
            splitH = false;
        else if (height > width && height / width >= 1.25)
            splitH = true;

        var max = (splitH ? height : width) - MIN_LEAF_SIZE; // determine the maximum height or width

        if (max <= MIN_LEAF_SIZE)
            return false; // the area is too small to split any more...		

        // determine where we're going to split		
        var split = r.Next(MIN_LEAF_SIZE, max);

        // create our left and right children based on the direction of the split		
        if (splitH)
        {
            leftChild = new Leaf(x, y, width, split);
            rightChild = new Leaf(x, y + split, width, height - split);
        }
        else
        {
            leftChild = new Leaf(x, y, split, height);
            rightChild = new Leaf(x + split, y, width - split, height);
        }

        return true;
    }
}

