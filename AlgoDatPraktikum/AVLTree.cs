using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDatPraktikum
{
    class AVLTree : BinSearchTree
    {
        bool elementOperationNotNecessary = false;  // boolean flag, intended for special case in Insert- and Delete-function

        public AVLTree() { }

        // Rotations for balancing of unbalanced tree

        Node RotateLeft(Node parent)
        {
            Node child = parent.left;
            parent.left = child.right;
            child.right = parent;
            return child;
        }

        Node RotateRight(Node parent)
        {
            Node child = parent.right;
            parent.right = child.left;
            child.left = parent;
            return child;
        }

        Node RotateLeftRight(Node parent)
        {
            Node child = parent.left;
            parent.left = RotateRight(child);
            return RotateLeft(parent);
        }

        Node RotateRightLeft(Node parent)
        {
            Node child = parent.right;
            parent.right = RotateLeft(child);
            return RotateRight(parent);
        }


        // gets the height of any subtree recursive in a recursive way
        // is used to decide balancefactor


        int GetHeight(Node node)
        {
            int treeHeight = 0;
            
            if (node != null)
            {
                int leftSide = GetHeight(node.left);
                int rightSide = GetHeight(node.right);

                treeHeight = ((leftSide > rightSide) ? leftSide : rightSide) + 1;
            }

            return treeHeight;
        }

        // gets the individual balancefactors, to decide whether rotation is necessary
        // BFaktor=0 : balanced;   B>1 : heavy on leftside;   B<1: heavy on rightside
        int BalanceFactor(Node node)
        {
            int leftSide = GetHeight(node.left);
            int rightSide = GetHeight(node.right);
            return leftSide - rightSide;
        }

        // balance tree with rotations where necessary
        Node BalanceTree(Node node)
        {
            int bFactor = BalanceFactor(node);

            if (bFactor > 1)
            {
                if (BalanceFactor(node.left) > 0)
                    node = RotateLeft(node);
                else
                    node = RotateLeftRight(node);
            }

            else if (bFactor < -1)
            {
                if (BalanceFactor(node.right) > 0)
                    node = RotateRightLeft(node);
                else
                    node = RotateRight(node);
            }

            return node;
        }

        public override bool Search(int key)
        {
            if (RecursiveSearch(key, root) == null)
                return false;

            return true;
        }

        Node RecursiveSearch(int key, Node node)
        {
            if (node == null)
                return node;

            else if (key == node.value)
                return node;

            else if (key < node.value)
                return RecursiveSearch(key, node.left);

            else

                return RecursiveSearch(key, node.right);              
        }

        public override bool Insert(int elem)
        {
            Node newNode = new Node(elem);

            if (root == null)
            {
                root = newNode;
                return true;
            }

            Node tempWurzel = root;

            tempWurzel = RecursiveInsert(tempWurzel, newNode);


            if(elementOperationNotNecessary)  // if element is already in the tree
                return false;


            root = tempWurzel;
            return true;
        }


        Node RecursiveInsert(Node node, Node newNode)
        {
            if (node == null)  // element should be at this position
            {
                node = newNode;
                return node;
            }

            else if (node.value == newNode.value)  // if element is already in tree
            {
                elementOperationNotNecessary = true;
                return node;
            }


            else if (newNode.value < node.value)   // left subtree
            {
                node.left = RecursiveInsert(node.left, newNode);
                node = BalanceTree(node);
            }

            else if (newNode.value > node.value)   // right subtree
            {
                node.right = RecursiveInsert(node.right, newNode);
                node = BalanceTree(node);
            }

            return node;
        }

        // TODO: improve performance
        public override bool Delete(int elem)
        {

            // -----------------  TODO: use bool elementOperationNotNecessary instead of searching two times!
            if (!Search(elem))
                return false;
            // -----------------


            if (root == null)
                return false;

            root = RecursiveDelete(root, elem);

            return true;
        }


        Node RecursiveDelete(Node node, int elem)
        {
            Node predecessor;

            if (node == null)  
            {
                return null;
            }

            if (elem < node.value)  // Suche linker Teilbaum
            {
                node.left = RecursiveDelete(node.left, elem);

                if (BalanceFactor(node) == -2)
                {
                    if (BalanceFactor(node.right) <= 0)
                        node = RotateRight(node);
                    else
                        node = RotateRightLeft(node);
                }
            }

            else if (elem > node.value)  // Suche rechter Teilbaum
            {
                //if (node.rechts == null)
                //    return node; 

                node.right = RecursiveDelete(node.right, elem);

                if (BalanceFactor(node) == 2)
                {
                    if (BalanceFactor(node.left) >= 0)
                        node = RotateLeft(node);

                    else
                        node = RotateLeftRight(node);

                }
            }

            else  // das gesuchte Element wurde gefunden (node.wert == elem)
            {
                if (node.right != null)
                {
                    predecessor = node.right;  // eigentlicher Löschvorgang

                    while (predecessor.left != null)
                        predecessor = predecessor.left;


                    node.value = predecessor.value;
                    node.right = RecursiveDelete(node.right, predecessor.value);

                    if (BalanceFactor(node) == 2)
                    {
                        if (BalanceFactor(node.left) >= 0)
                            node = RotateLeft(node);

                        else
                            node = RotateLeftRight(node);
                    }

                }

                else  // Fall, node.links != null
                {
                    return node.left;
                }
        
            }
            return node;
        }
    }
}
