package com.segimara;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;
class Engle implements Comparable<Engle> {
    int u;
    int v;
    int w;

    /* Конструктор */
    Engle(int u, int v, int w) {
        this.u = u;
        this.v = v;
        this.w = w;
    }
    /* Компаратор */
    @Override
    public int compareTo(Engle edge) {
        if (w != edge.w) return w < edge.w ? -1 : 1;
        return 0;
    }
}

/* Класс СНМ */
class Extentions {
    int[] Leads;
    int[] Ransk;

    Extentions(int size) {
        Leads = new int [size];
        Ransk = new int [size];
        for (int i = 0; i < size; i++)
            Leads[i] = i;
    }

    int set(int x) {
        return x == Leads[x] ? x : (Leads[x] = set(Leads[x]));
    }

    boolean union(int u, int v) {
        if ((u = set(u)) == (v = set(v)))
            return false;
        if (Ransk[u] < Ransk[v]) {
            Leads[u] = v;
        } else {
            Leads[v] = u;
            if (Ransk[u] == Ransk[v])
                Ransk[u]++;
        }
        return true;
    }
}
public class TaskK {

    public static Scanner scan = new Scanner(System.in);
    /* Алгоритм Краскала за O(E log E) */
    public static void main1(String[] args) {
        int n = Integer.parseInt(scan.next());
        int[] c = new int[n];
        int[] a = new int[n];
        for (int i = 0; i < n; i++) {
            c[i] = Integer.parseInt(scan.next());
        }
        for (int i = 0; i < n; i++) {
            a[i] = Integer.parseInt(scan.next());
        }

        ArrayList<Engle> graf = new ArrayList<>();
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if (c[i] != c[j])
                {
                    graf.add(new Engle(i, j, Math.abs(a[i] - a[j])));

                }
                else
                {

                }
            }

        }
        System.out.println(Krusk(graf.toArray(new Engle[0])));
    }
    static int Krusk(Engle[] edges) {
        Extentions dsf = new Extentions(edges.length);
        Arrays.sort(edges);
        int ret = 0;
        for (int i = 0; i < edges.length; i++) {
            if (dsf.union(edges[i].u, edges[i].v))
                ret += edges[i].w;
        }
        return ret;
    }

}
