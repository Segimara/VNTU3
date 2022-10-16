package com.segimara;

import org.junit.Test;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

import static org.junit.Assert.assertEquals;

public class PingPong_6kyu
{
    public static void Main(String[] args) {
        PingPongTest.test();
    }
}
class PingPongTest {
    @Test
    public static void test() {
        ArrayList<Score> result = new ArrayList<>();
        result.add(new Score(11, 0));
        result.add(new Score(11, 0));
        result.add(new Score(1, 1));
        result.add(new Score(21, 0));
        result.add(new Score(2, 1));
        assertEquals(result, PingPong.result("WWWWWWWWWWWWWWWWWWWWWWLW"));
        result = new ArrayList<>();
        result.add(new Score(11, 0));
        result.add(new Score(0, 11));
        result.add(new Score(11, 11));
        assertEquals(result, PingPong.result("WWWWWWWWWWWLLLLLLLLLLL"));
        result = new ArrayList<>();
        result.add(new Score(9, 11));
        result.add(new Score(1, 0));
        result.add(new Score(10, 11));
        assertEquals(result, PingPong.result("WLWLLLWWWLWLWLLLWLWLW"));

    }
}
class PingPong {
    public static List<Score> result(String winOrLose) {
        List<Score> res = new LinkedList<>();

        res.addAll(GenericResult(winOrLose, 11));
        res.addAll(GenericResult(winOrLose, 21));

        return res;
    }
    public static List<Score> GenericResult(String winOrLose, int scoringSystem)
    {
        List<Score> res = new LinkedList<>();
        int first = 0;
        int second = 0;
        for (char letter: winOrLose.toCharArray())
        {
            if (letter == 'W')
            {
                first++;
            }
            else
            {
                second++;
            }
            if (first >= scoringSystem || second >= scoringSystem)
            {
                if (scoringSystem != 11)
                {
                    res.add(new Score(first, second));
                    first = 0;
                    second = 0;
                }
                else
                {
                    if (Math.abs(first - second) == 2)
                    {
                        res.add(new Score(first, second));
                        first = 0;
                        second = 0;
                    }
                }
            }
        }
        if (first != 0 && second != 0)
        {
            res.add(new Score(first, second));
        }
        return res;
    }
}

class Score
{

    public Score(int me, int opponent)
    {

    }

    @Override
    public boolean equals(Object obj) {
        return super.equals(obj);
    }
}
