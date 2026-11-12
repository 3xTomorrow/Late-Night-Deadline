import random

def create_add_sub_probs(add_sub_nums: list[int], subtraction: bool = False) -> dict[str,int]:
    equations: dict[str,int] = dict()
    nums: list[int] = add_sub_nums.copy()
    while len(nums) > 0:
        num1: int = random.choice(nums)
        nums.remove(num1)
        num2: int = random.choice(nums)
        nums.remove(num2)

        if subtraction:
            solution: int = num1 - num2
            equation_str: str = str(num1) + " - " + str(num2)
        else:
            solution: int = num1 + num2
            equation_str: str = str(num1) + " + " + str(num2)

        equations[equation_str] = solution

    return equations

def create_mult_probs(mult_nums: list[int]):
    equations: dict[str,int] = dict()
    nums: list[int] = mult_nums.copy()
    while len(nums) > 0:
        num1: int = random.choice(nums)
        nums.remove(num1)
        num2: int = random.choice(nums)
        nums.remove(num2)

        solution: int = num1*num2
        equation_str: str = str(num1) + " x " + str(num2)

        equations[equation_str] = solution

    return equations

def create_div_probs(div_nums: list[int]):
    equations: dict[str,int | float] = dict()
    nums: list[int] = div_nums.copy()
    while len(nums) > 0:
        num1: int = random.choice(nums)
        num2: int = random.choice(nums)

        if num1>num2 :
            nums.remove(num1)
            nums.remove(num2)
            solution: int | float = round(num1 / num2,2)
            equation_str: str = str(num1) + " / " + str(num2)
            equations[equation_str] = solution

    return equations

def write_to_file(filename:str, equations: dict):
    with open(filename,'w') as f:
        for key in equations:
            f.write(key + " = " + str(equations[key]) + "\n")

def main():
    nums: list[int] = list(range(100))
    div_nums: list[int] = list(range(1,201))

    addition_equations: dict[str,int] = create_add_sub_probs(nums)
    subtraction_equations: dict[str,int] = create_add_sub_probs(nums,True)
    mult_equations: dict[str,int] = create_mult_probs(nums)
    div_equations: dict[str,int | float] = create_div_probs(div_nums)

    # open and clear addition file and add equations there.
    write_to_file("equations/addition.txt", addition_equations)
    write_to_file("equations/subtraction.txt",subtraction_equations)
    write_to_file('equations/multiplication.txt',mult_equations)
    write_to_file('equations/division.txt',div_equations)


if __name__ == '__main__':
    main()