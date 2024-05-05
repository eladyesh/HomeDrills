from math import pi


def reverse_n_pi_digits(n: int) -> str:
    """
    Reverse the first n digits of the mathematical constant pi.

    Args:
        n (int): Number of digits to reverse.

    Returns:
        str: A string representing the reversed first n digits of pi.
    """
    return str(pi)[:n + 1][::-1]


if __name__ == "__main__":
    print(reverse_n_pi_digits(5))  # Output: 5141.3
