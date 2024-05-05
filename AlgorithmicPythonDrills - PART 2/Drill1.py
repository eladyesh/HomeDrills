import math


def num_len(num) -> int:
    
    """
    Return the number of digits in a given number.

    Args:
        num (int): The number to find the length of.

    Returns:
        int: The number of digits in the input number.
    """

    # We use math.log10 for the number of times 10 has multiply it self to get to the number.
    # For instance, 10 ** 3 = 1000, so 3 + 1 = 4
    # 10 ** 2 = 100
    # 10 ** 4.99.. = 98635, so 4 + 1 = 5

    return 1 if num == 0 else int(math.log10(abs(num))) + 1


if __name__ == "__main__":
    
    print(num_len(100))  # print 3
    print(num_len(-475))  # print 3
    print(num_len(77777))  # print 5
