def pythagorean_triplet_by_sum(sum):
    """Prints all Pythagorean triplets that sum up to a given number.

    Args:
        sum (int): The desired sum of the Pythagorean triplets.

    Returns:
        None
    """
    # We start by iterating over the numbers from the smallest number (a) till sum.
    # Then, we iterate over the numbers that are bigger then a and not bigger then sum.
    # Check if c (which is sum - a - b) is bigger than b and if a ** 2 + b ** 2 == c ** 2
    for a in range(1, sum):
        for b in range(a, sum):
            c = sum - a - b
            if c > b:
                if a ** 2 + b ** 2 == c ** 2:
                    print(f"{a} < {b} < {c}")


if __name__ == "__main__":
    pythagorean_triplet_by_sum(12)  # 3 < 4 < 5
    pythagorean_triplet_by_sum(120)  # 20 < 48 < 52, 24 < 45 < 51, 30 < 40 < 50
