import mpmath


def reverse_n_pi_digits(n: int) -> str:
    
    """
    Reverse the first n digits of the mathematical constant pi.

    Args:
        n (int): Number of digits to reverse.

    Returns:
        str: A string representing the reversed first n digits of pi.
    """
    
    # Set the precision (number of digits) for pi
    mpmath.mp.dps = n + 1
    pi_str = str(mpmath.pi)
    return pi_str[::-1]


if __name__ == "__main__":
    print(reverse_n_pi_digits(5))  # Output: 5141.3
