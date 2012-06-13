// Copyright (c) 2006 Damien Miller <djm@mindrot.org>
// Copyright (c) 2007 Derek Slager
//
// Permission to use, copy, modify, and distribute this software for any
// purpose with or without fee is hereby granted, provided that the above
// copyright notice and this permission notice appear in all copies.
//
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

using System;
using NUnit.Framework;

/// <summary>NUnit unit tests for BCrypt routines.</summary>
[TestFixture]
public class BCryptTest
{

    string[][] testVectors = {
        new string[] { "",
                       "$2a$06$DCq7YPn5Rq63x1Lad4cll.",
                       "$2a$06$DCq7YPn5Rq63x1Lad4cll.TV4S6ytwfsfvkgY8jIucDrjc8deX1s." },
        new string[] { "",
                       "$2a$08$HqWuK6/Ng6sg9gQzbLrgb.",
                       "$2a$08$HqWuK6/Ng6sg9gQzbLrgb.Tl.ZHfXLhvt/SgVyWhQqgqcZ7ZuUtye" },
        new string[] { "",
                       "$2a$10$k1wbIrmNyFAPwPVPSVa/ze",
                       "$2a$10$k1wbIrmNyFAPwPVPSVa/zecw2BCEnBwVS2GbrmgzxFUOqW9dk4TCW" },
        new string[] { "",
                       "$2a$12$k42ZFHFWqBp3vWli.nIn8u",
                       "$2a$12$k42ZFHFWqBp3vWli.nIn8uYyIkbvYRvodzbfbK18SSsY.CsIQPlxO" },
        new string[] { "a",
                       "$2a$06$m0CrhHm10qJ3lXRY.5zDGO",
                       "$2a$06$m0CrhHm10qJ3lXRY.5zDGO3rS2KdeeWLuGmsfGlMfOxih58VYVfxe" },
        new string[] { "a",
                       "$2a$08$cfcvVd2aQ8CMvoMpP2EBfe",
                       "$2a$08$cfcvVd2aQ8CMvoMpP2EBfeodLEkkFJ9umNEfPD18.hUF62qqlC/V." },
        new string[] { "a",
                       "$2a$10$k87L/MF28Q673VKh8/cPi.",
                       "$2a$10$k87L/MF28Q673VKh8/cPi.SUl7MU/rWuSiIDDFayrKk/1tBsSQu4u" },
        new string[] { "a",
                       "$2a$12$8NJH3LsPrANStV6XtBakCe",
                       "$2a$12$8NJH3LsPrANStV6XtBakCez0cKHXVxmvxIlcz785vxAIZrihHZpeS" },
        new string[] { "abc",
                       "$2a$06$If6bvum7DFjUnE9p2uDeDu",
                       "$2a$06$If6bvum7DFjUnE9p2uDeDu0YHzrHM6tf.iqN8.yx.jNN1ILEf7h0i" },
        new string[] { "abc",
                       "$2a$08$Ro0CUfOqk6cXEKf3dyaM7O",
                       "$2a$08$Ro0CUfOqk6cXEKf3dyaM7OhSCvnwM9s4wIX9JeLapehKK5YdLxKcm" },
        new string[] { "abc",
                       "$2a$10$WvvTPHKwdBJ3uk0Z37EMR.",
                       "$2a$10$WvvTPHKwdBJ3uk0Z37EMR.hLA2W6N9AEBhEgrAOljy2Ae5MtaSIUi" },
        new string[] { "abc",
                       "$2a$12$EXRkfkdmXn2gzds2SSitu.",
                       "$2a$12$EXRkfkdmXn2gzds2SSitu.MW9.gAVqa9eLS1//RYtYCmB1eLHg.9q" },
        new string[] { "abcdefghijklmnopqrstuvwxyz",
                       "$2a$06$.rCVZVOThsIa97pEDOxvGu",
                       "$2a$06$.rCVZVOThsIa97pEDOxvGuRRgzG64bvtJ0938xuqzv18d3ZpQhstC" },
        new string[] { "abcdefghijklmnopqrstuvwxyz",
                       "$2a$08$aTsUwsyowQuzRrDqFflhge",
                       "$2a$08$aTsUwsyowQuzRrDqFflhgekJ8d9/7Z3GV3UcgvzQW3J5zMyrTvlz." },
        new string[] { "abcdefghijklmnopqrstuvwxyz",
                       "$2a$10$fVH8e28OQRj9tqiDXs1e1u",
                       "$2a$10$fVH8e28OQRj9tqiDXs1e1uxpsjN0c7II7YPKXua2NAKYvM6iQk7dq" },
        new string[] { "abcdefghijklmnopqrstuvwxyz",
                       "$2a$12$D4G5f18o7aMMfwasBL7Gpu",
                       "$2a$12$D4G5f18o7aMMfwasBL7GpuQWuP3pkrZrOAnqP.bmezbMng.QwJ/pG" },
        new string[] { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD",
                       "$2a$06$fPIsBO8qRqkjj273rfaOI.",
                       "$2a$06$fPIsBO8qRqkjj273rfaOI.HtSV9jLDpTbZn782DC6/t7qT67P6FfO" },
        new string[] { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD",
                       "$2a$08$Eq2r4G/76Wv39MzSX262hu",
                       "$2a$08$Eq2r4G/76Wv39MzSX262huzPz612MZiYHVUJe/OcOql2jo4.9UxTW" },
        new string[] { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD",
                       "$2a$10$LgfYWkbzEvQ4JakH7rOvHe",
                       "$2a$10$LgfYWkbzEvQ4JakH7rOvHe0y8pHKF9OaFgwUZ2q7W2FFZmZzJYlfS" },
        new string[] { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD",
                       "$2a$12$WApznUOJfkEGSmYRfnkrPO",
                       "$2a$12$WApznUOJfkEGSmYRfnkrPOr466oFDCaj4b6HY3EXGvfxm43seyhgC" },
    };

    /// <summary>Test method for <c>BCrypt.HashPassword(String,
    /// String)</c>.</summary>
    [Test]
    public void TestHashPassword()
    {
        Console.Write("BCrypt.HashPassword(): ");
        for (int i = 0; i < testVectors.Length; i++)
        {
            string plain = testVectors[i][0];
            string salt = testVectors[i][1];
            string expected = testVectors[i][2];
            string hashed = BCrypt.HashPassword(plain, salt);
            Assert.AreEqual(hashed, expected);
            Console.Write(".");
        }
        Console.WriteLine();
    }

    /// <summary>Test method for <c>BCrypt.GenerateSalt(int)</c>.</summary>
    [Test]
    public void TestGenerateSaltInt()
    {
        Console.Write("BCrypt.GenerateSalt(logRounds):");
        for (int i = 4; i <= 12; i++)
        {
            Console.Write(" " + i + ":");
            for (int j = 0; j < testVectors.Length; j += 4)
            {
                string plain = testVectors[j][0];
                string salt = BCrypt.GenerateSalt(i);
                string hashed1 = BCrypt.HashPassword(plain, salt);
                string hashed2 = BCrypt.HashPassword(plain, hashed1);
                Assert.AreEqual(hashed1, hashed2);
                Console.Write(".");
            }
        }
        Console.WriteLine();
    }

    /// <summary>Test method for <c>BCrypt.GenerateSalt()</c>.</summary>
    [Test]
    public void TestGenerateSalt()
    {
        Console.Write("BCrypt.GenerateSalt(): ");
        for (int i = 0; i < testVectors.Length; i += 4)
        {
            string plain = testVectors[i][0];
            string salt = BCrypt.GenerateSalt();
            string hashed1 = BCrypt.HashPassword(plain, salt);
            string hashed2 = BCrypt.HashPassword(plain, hashed1);
            Assert.AreEqual(hashed1, hashed2);
            Console.Write(".");
        }
        Console.WriteLine();
    }


    /// <summary>Test method for <c>BCrypt.CheckPassword(String,
    /// String)</c> expecting success.</summary>
    [Test]
    public void TestCheckPasswordSuccess()
    {
        Console.Write("BCrypt.CheckPassword w/ good passwords: ");
        for (int i = 0; i < testVectors.Length; i++)
        {
            string plain = testVectors[i][0];
            string expected = testVectors[i][2];
            Assert.IsTrue(BCrypt.CheckPassword(plain, expected));
            Console.Write(".");
        }
        Console.WriteLine();
    }

    /// <summary>Test method for <c>BCrypt.CheckPassword(String,
    /// String)</c> expecting failure.</summary>
    [Test]
    public void TestCheckPasswordFailure()
    {
        Console.Write("BCrypt.CheckPassword w/ bad passwords: ");
        for (int i = 0; i < testVectors.Length; i++)
        {
            int broken_index = (i + 4) % testVectors.Length;
            string plain = testVectors[i][0];
            string expected = testVectors[broken_index][2];
            Assert.IsFalse(BCrypt.CheckPassword(plain, expected));
            Console.Write(".");
        }
        Console.WriteLine();
    }

}
