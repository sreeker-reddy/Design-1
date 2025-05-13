/* Time Complexity: O(log(n/2003) = O(log(n))
   Space Complexity: O(2003+n) = O(n)

   Approach: Choosing a prime number as the number of BST trees enables a more uniform distribution. As binary search trees are more efficient at operations performed on them for searching,
             adding and removing, it is a good choice for a HashSet.
*/

public class Node
{
    public int val;
    public Node left;
    public Node right;
    public Node(int val)
    {
        this.val = val;
        this.left = null;
        this.right = null;
    }
}

public class BST
{
    public Node node;

    public Node Search(Node root, int val)
    {
        if(root==null || root.val==val)
        {
            return root;
        }
        return val<root.val ? Search(root.left,val) : Search(root.right,val);
    }

    public Node Add(Node root, int val)
{
    if (root == null)
        return new Node(val);

    if (val < root.val)
        root.left = Add(root.left, val);
    else if (val > root.val)
        root.right = Add(root.right, val);
    return root;
}

    public Node Delete(Node root, int val)
    {
        if(root==null)
        {
            return null;
        }

        if(val>root.val)
        {
            root.right = Delete(root.right, val);
        }
        else
        {
            if(val<root.val)
            {
                root.left = Delete(root.left, val);
            }
            else
            {
                if(root.left==null && root.right==null)
                    root = null;
                else
                {
                    if(root.right!=null)
                    {
                        root.val = successor(root,val);
                        root.right = Delete(root.right, root.val);
                    }
                    else
                    {
                        root.val = predecessor(root,val);
                        root.left = Delete(root.left, root.val);
                    }
                }
            }
            
        }
        return root;
    }

    private int successor(Node root, int val)
    {
        root = root.right;
        while(root.left!=null)
            root = root.left;
        
        return root.val;
    }

    private int predecessor(Node root,int val)
    {
        root = root.left;
        while(root.right!=null)
            root= root.right;

        return root.val;
    }
}


public class MyHashSet {
    private readonly int HASH = 2003; //prime number
    BST[] trees;
    public MyHashSet() {
        trees = new BST[HASH];
        for(int i=0;i<trees.Length;i++)
            trees[i] = new BST();
    }
    
    public int hash(int key)
    {
        return key%HASH;
    }

    public void Add(int key) 
    {
        int hashValue = hash(key);
        Node root = trees[hashValue].node;
        trees[hashValue].node = trees[hashValue].Add(root, key); // Fix: assign back
    }

    public void Remove(int key) 
    {
        int hashValue = hash(key);
        Node root = trees[hashValue].node;
        trees[hashValue].node = trees[hashValue].Delete(root, key); // Fix: assign back
    }

    public bool Contains(int key) 
    {
        int hashValue = hash(key);
        Node root = trees[hashValue].node;
        root = trees[hashValue].Search(root, key);
        return root != null;
    }
}
