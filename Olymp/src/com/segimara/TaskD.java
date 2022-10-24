package com.segimara;

import java.util.Arrays;
import java.util.Scanner;

public class TaskD {
    static Scanner sc = new Scanner(System.in);

    public static void main1(String[] args) {
        int[] nq = new int[2];
        for (int i = 0; i < 2; i++) {
            nq[i] = Integer.parseInt(sc.next());
        }
        long[] integers = new long[nq[0]];
        for (int i = 0; i < nq[0]; i++) {
            integers[i] = Long.parseLong(sc.next());
        }

        Arrays.sort(integers);
        long x = 0;
        for (int i = 0; i < nq[1]; i++) {
            long[] query = new long[2];
            for (int j = 0;j < 2; j++) {
                query[j] = Long.parseLong(sc.next());
            }
            if (query[0] == 1) {
               x += query[1];
            }
            else if (query[0] == 2) {
                System.out.println(integers[integers.length - ((int) query[1])] + x);
            }
        }
    }
}