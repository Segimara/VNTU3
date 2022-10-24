package com.segimara;


import java.util.Arrays;
import java.util.Scanner;

public class TaskC {
    static Scanner sc = new Scanner(System.in);
    static class Point {
        long x;
        long y;
        Point(long x, long y) {
            this.x = x;
            this.y = y;
        }
    }
    public static void main(String[] args) {
        int t = Integer.parseInt(sc.nextLine());
        for (int i = 0; i < t; i++) {
            int n = sc.nextInt();
            Point[] points = new Point[n];
            for (int j = 0; j < n; j++) {
                long[] coord = new long[2];
                for (int k = 0; k < 2; k++) {
                    coord[k] = Long.parseLong(sc.next());
                }
                points[j] = new Point(coord[0], coord[1]);
            }

            double[] distances = new double[n];
            for (int k = 0; k < n; k++) {
                distances[k] = Double.NaN;
            }
            for (int j = 0; j < n; j++) {
                Point p = points[j];
                double min = -1;
                for (int k = 0; k < n; k++) {
                    if (j == k) continue;
                    Point q = points[k];
                    double dist = distance(p.x, q.x, p.y, q.y);
                    if (min == -1 || (Double.isNaN(distances[j]) || Double.isNaN(distances[k])) || dist < min) {
                        min = dist;
                        if ((Double.isNaN(distances[j]) || Double.isNaN(distances[k]))) {
                            distances[j] = min;
                            distances[k] = min;

                        }
                        if (dist < distances[j]) {
                            distances[j] = min;
                        }
                        if (dist < distances[k]) {
                            distances[k] = min;
                        }
                    }
                }
            }

            for (int j = 0; j < n; j++) {
                System.out.printf("%.6f ", distances[j]);
            }
            System.out.println();
        }
    }
    static double distance(long xi, long xj, long yi, long yj) {
        return Math.sqrt(Math.pow(Math.abs(xi - xj), 2) + Math.pow(Math.abs(yi - yj), 2));
    }
}