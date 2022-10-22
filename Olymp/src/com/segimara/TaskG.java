package com.segimara;

import java.util.ArrayList;
import java.util.Scanner;

public class TaskG
{
    public static Scanner scan = new Scanner(System.in);

    public static void main1(String args[])
    {
        int t = scan.nextInt();
        ArrayList<int[]> arrsN = new ArrayList<>(t);
        ArrayList<int[]> arrsM = new ArrayList<>(t);
        for (int i = 0; i < t; i++)
        {
            int n = scan.nextInt();
            int m = scan.nextInt();
            arrsN.set(i, new int[n]);
            arrsM.set(i, new int[m]);
            for (int j = 0; j < n; j++) {
                arrsN.get(i)[j] = scan.nextInt();
            }
            for (int j = 0; j < m; j++) {
                arrsM.get(i)[j] = scan.nextInt();
            }
        }
        for (int i = 0; i < t; i++) {
            int resXORN = 0;
            int resXORM = 0;
            int sumN = 0;
            int sumM = 0;
            for (int j = 0; j < arrsN.get(i).length; j++) {
                resXORN ^= arrsN.get(i)[j];
                sumN += arrsN.get(i)[j];
            }
            for (int j = 0; j < arrsM.get(i).length; j++) {
                resXORN ^= arrsN.get(i)[j];
                sumM += arrsN.get(i)[j];
            }

        }

    }
}
