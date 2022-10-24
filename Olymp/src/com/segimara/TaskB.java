package com.segimara;

import java.util.Scanner;

public class TaskB {
    static Scanner scan = new Scanner(System.in);
    public static void main(String[] args) {
        int n = scan.nextInt();
        for (int j = 0; j < n; j++) {
            int t = scan.nextInt();
            int[] nums = new int[t];
            for (int i = 0; i < t; i++) {
                nums[i] = scan.nextInt();
            }
            int res = (nums[0] == nums[t-1])? t*(t-1) : t*t;
            for (int i = 0; i < t; i++) {
                if (nums[i] == 0)
                {
                    res--;
                }
            }

            System.out.println(res);
        }
    }
}
