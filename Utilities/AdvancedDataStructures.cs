// AdvancedDataStructures.cs - Fixed
using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp.Data.Structures
{
    // Basic Tree Node
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T data)
        {
            Data = data;
            Children = new List<TreeNode<T>>();
        }
    }

    // Binary Tree Node
    public class BinaryTreeNode<T>
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }
    }

    // Binary Search Tree for Service Requests
    public class ServiceRequestBST
    {
        private BinaryTreeNode<ServiceRequest> root;

        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private BinaryTreeNode<ServiceRequest> InsertRec(BinaryTreeNode<ServiceRequest> node, ServiceRequest request)
        {
            if (node == null)
            {
                return new BinaryTreeNode<ServiceRequest>(request);
            }

            // Compare by creation date for chronological ordering
            if (request.CreatedDate.CompareTo(node.Data.CreatedDate) < 0)
            {
                node.Left = InsertRec(node.Left, request);
            }
            else
            {
                node.Right = InsertRec(node.Right, request);
            }

            return node;
        }

        public List<ServiceRequest> InOrderTraversal()
        {
            var result = new List<ServiceRequest>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(BinaryTreeNode<ServiceRequest> node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Data);
                InOrderRec(node.Right, result);
            }
        }

        public ServiceRequest Search(string requestId)
        {
            return SearchRec(root, requestId);
        }

        private ServiceRequest SearchRec(BinaryTreeNode<ServiceRequest> node, string requestId)
        {
            if (node == null) return null;

            if (node.Data.Id == requestId)
                return node.Data;

            var leftResult = SearchRec(node.Left, requestId);
            if (leftResult != null) return leftResult;

            return SearchRec(node.Right, requestId);
        }
    }

    // AVL Tree for Priority-based organization
    public class PriorityAVLTree
    {
        private AVLTreeNode root;

        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private AVLTreeNode InsertRec(AVLTreeNode node, ServiceRequest request)
        {
            if (node == null)
                return new AVLTreeNode(request);

            // Compare by priority (lower number = higher priority)
            if (request.Priority.CompareTo(node.Data.Priority) < 0)
                node.Left = InsertRec(node.Left, request);
            else if (request.Priority.CompareTo(node.Data.Priority) > 0)
                node.Right = InsertRec(node.Right, request);
            else
                node.Right = InsertRec(node.Right, request); // Same priority, insert to right

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            int balance = GetBalance(node);

            // Left Left Case
            if (balance > 1 && request.Priority.CompareTo(node.Left.Data.Priority) < 0)
                return RightRotate(node);

            // Right Right Case
            if (balance < -1 && request.Priority.CompareTo(node.Right.Data.Priority) > 0)
                return LeftRotate(node);

            // Left Right Case
            if (balance > 1 && request.Priority.CompareTo(node.Left.Data.Priority) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && request.Priority.CompareTo(node.Right.Data.Priority) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private int GetHeight(AVLTreeNode node)
        {
            return node?.Height ?? 0;
        }

        private int GetBalance(AVLTreeNode node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        private AVLTreeNode RightRotate(AVLTreeNode y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        private AVLTreeNode LeftRotate(AVLTreeNode x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        public List<ServiceRequest> GetHighPriorityRequests()
        {
            var result = new List<ServiceRequest>();
            GetHighPriorityRec(root, result);
            return result;
        }

        private void GetHighPriorityRec(AVLTreeNode node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                GetHighPriorityRec(node.Left, result); // Left has higher priority
                if (node.Data.Priority <= 2) // Critical and High priority
                    result.Add(node.Data);
                GetHighPriorityRec(node.Right, result);
            }
        }

        // AVL Tree Node (non-generic version) - (GeeksforGeeks, 2017).
        public class AVLTreeNode
        {
            public ServiceRequest Data { get; set; }
            public AVLTreeNode Left { get; set; }
            public AVLTreeNode Right { get; set; }
            public int Height { get; set; }

            public AVLTreeNode(ServiceRequest data)
            {
                Data = data;
                Height = 1;
            }
        }
    }

    // Min-Heap for priority queue
    public class PriorityHeap
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();

        public void Enqueue(ServiceRequest request)
        {
            heap.Add(request);
            int i = heap.Count - 1;
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (heap[parent].Priority <= heap[i].Priority)
                    break;

                Swap(i, parent);
                i = parent;
            }
        }

        public ServiceRequest Dequeue()
        {
            if (heap.Count == 0) return null;

            var result = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            int i = 0;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int smallest = i;

                if (left < heap.Count && heap[left].Priority < heap[smallest].Priority)
                    smallest = left;
                if (right < heap.Count && heap[right].Priority < heap[smallest].Priority)
                    smallest = right;

                if (smallest == i) break;

                Swap(i, smallest);
                i = smallest;
            }

            return result;
        }

        public ServiceRequest Peek()
        {
            return heap.Count > 0 ? heap[0] : null;
        }

        public int Count => heap.Count;

        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }

    // Graph for service department relationships
    public class ServiceDepartmentGraph
    {
        private Dictionary<string, List<(string department, double weight)>> adjacencyList = new Dictionary<string, List<(string, double)>>();

        public void AddDepartment(string department)
        {
            if (!adjacencyList.ContainsKey(department))
                adjacencyList[department] = new List<(string, double)>();
        }

        public void AddCollaboration(string dept1, string dept2, double collaborationStrength)
        {
            AddDepartment(dept1);
            AddDepartment(dept2);

            adjacencyList[dept1].Add((dept2, collaborationStrength));
            adjacencyList[dept2].Add((dept1, collaborationStrength));
        }

        public List<string> BreadthFirstSearch(string startDepartment)
        {
            var visited = new HashSet<string>();
            var result = new List<string>();
            var queue = new Queue<string>();

            visited.Add(startDepartment);
            queue.Enqueue(startDepartment);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor.department))
                    {
                        visited.Add(neighbor.department);
                        queue.Enqueue(neighbor.department);
                    }
                }
            }

            return result;
        }

        public List<string> DepthFirstSearch(string startDepartment)
        {
            var visited = new HashSet<string>();
            var result = new List<string>();
            DFSRec(startDepartment, visited, result);
            return result;
        }

        private void DFSRec(string department, HashSet<string> visited, List<string> result)
        {
            visited.Add(department);
            result.Add(department);

            foreach (var neighbor in adjacencyList[department])
            {
                if (!visited.Contains(neighbor.department))
                {
                    DFSRec(neighbor.department, visited, result);
                }
            }
        }

        // Prim's Algorithm for Minimum Spanning Tree
        public List<(string from, string to, double weight)> GetMinimumSpanningTree()
        {
            var mst = new List<(string, string, double)>();
            var visited = new HashSet<string>();
            var edges = new EdgePriorityHeap();

            if (adjacencyList.Count == 0) return mst;

            // Start with first department
            var startDept = adjacencyList.Keys.First();
            visited.Add(startDept);

            // Add all edges from start department
            foreach (var neighbor in adjacencyList[startDept])
            {
                edges.Enqueue((neighbor.weight, startDept, neighbor.department));
            }

            while (visited.Count < adjacencyList.Count && edges.Count > 0)
            {
                var edge = edges.Dequeue();

                if (visited.Contains(edge.toDept)) continue;

                visited.Add(edge.toDept);
                mst.Add((edge.fromDept, edge.toDept, edge.weight));

                // Add all edges from the new department
                foreach (var neighbor in adjacencyList[edge.toDept])
                {
                    if (!visited.Contains(neighbor.department))
                    {
                        edges.Enqueue((neighbor.weight, edge.toDept, neighbor.department));
                    }
                }
            }

            return mst;
        }

        // Priority Heap for edges (for MST)
        private class EdgePriorityHeap
        {
            private List<(double weight, string fromDept, string toDept)> heap = new List<(double, string, string)>();

            public void Enqueue((double weight, string fromDept, string toDept) item)
            {
                heap.Add(item);
                int i = heap.Count - 1;
                while (i > 0)
                {
                    int parent = (i - 1) / 2;
                    if (heap[parent].weight <= heap[i].weight)
                        break;

                    Swap(i, parent);
                    i = parent;
                }
            }

            public (double weight, string fromDept, string toDept) Dequeue()
            {
                if (heap.Count == 0) return (0, null, null);

                var result = heap[0];
                heap[0] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);

                int i = 0;
                while (true)
                {
                    int left = 2 * i + 1;
                    int right = 2 * i + 2;
                    int smallest = i;

                    if (left < heap.Count && heap[left].weight < heap[smallest].weight)
                        smallest = left;
                    if (right < heap.Count && heap[right].weight < heap[smallest].weight)
                        smallest = right;

                    if (smallest == i) break;

                    Swap(i, smallest);
                    i = smallest;
                }

                return result;
            }

            public int Count => heap.Count;

            private void Swap(int i, int j)
            {
                var temp = heap[i];
                heap[i] = heap[j];
                heap[j] = temp;
            }
        }
    }
}


// Code Atribution
/*
    1. GeeksforGeeks. 2017. Count greater nodes in AVL tree. [online] GeeksforGeeks. Available at: https://www.geeksforgeeks.org/dsa/count-greater-nodes-in-avl-tree/ [Accessed 10 Nov. 2025].
 */