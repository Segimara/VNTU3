package com.segimara;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

public class TaskL {
    public static Scanner scan = new Scanner(System.in);

    public static void main1(String[] args) {
        int t = Integer.parseInt(scan.next());
        ArrayList<int[]> arrsN = new ArrayList<>(t);
        ArrayList<Integer> results = new ArrayList<>(t);
        ArrayList<Integer> Counts = new ArrayList<>(t);
        for (int i = 0; i < t; i++)
        {
            int n = Integer.parseInt(scan.next());
            arrsN.add( new int[n]);
            results.add(0);
            Counts.add(1);
            int tmp = 0;
            for (int j = 0; j < n; j++) {
                arrsN.get(i)[j] = Integer.parseInt(scan.next());
                if (tmp == 0)
                {
                    ++tmp;
                    if (Counts.get(i) > 1)
                    {
                        results.set(i, results.get(i)+1);
                        Counts.set(i, 1);
                        tmp = 0;
                    }
                }
                else
                {
                    if (arrsN.get(i)[j-1] > arrsN.get(i)[j])
                    {
                        Counts.set(i, Counts.get(i)+1);
                        tmp = 0;
                        if (Counts.get(i) > 1)
                        {
                            results.set(i, results.get(i)+1);
                            Counts.set(i, 1);
                            tmp = 0;
                        }
                    }
                    tmp = 0;
                }
            }
            tmp = 0;

        }
        for (int i = 0; i < t; i++) {
            System.out.println(results.get(i));
        }
    }
}
