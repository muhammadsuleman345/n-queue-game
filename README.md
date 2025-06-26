# ♛ N Queen Game

A Windows Forms application developed in C# that solves the classic **N-Queens Problem** through an interactive and educational interface. This project was created as part of the Data Structures and Algorithms (CSC-221) course at Bahria University, Fall 2024.

## 🧠 Problem Statement

The goal is to place **N queens on an N×N chessboard** so that no two queens threaten each other — i.e., no two queens share the same row, column, or diagonal.

## 🧩 Features

- 👩‍💻 **Manual Gameplay** – Click to place/remove queens interactively.
- 🤖 **Auto-Solve** – Solves the N-Queens puzzle using the **Backtracking Algorithm**.
- 💡 **Hint System** – Gives intelligent suggestions for possible placements based on difficulty.
- 🧱 **Three Difficulty Levels**:
  - Easy (4x4)
  - Medium (8x8)
  - Hard (12x12)
- 🎯 **Victory Check** – Displays a Congratulations screen upon successful placement.
- 🖱️ **GUI** – Developed with C# and Windows Forms for a clean and intuitive user experience.

## 🔧 Tech Stack

- Language: C#
- Framework: .NET Framework (Windows Forms)
- Concepts Used: Object-Oriented Programming (OOP), Backtracking Algorithm, Event-driven Programming

## 🧮 Algorithm Used

The core logic uses a **recursive backtracking algorithm**:
1. Try to place a queen in each row.
2. Use the `IsSafe` function to validate the position (column & diagonal checks).
3. If no position is valid in a row, **backtrack**.
4. If all queens are placed, a solution is found.


# 📚 Educational Value
This project helps in:

Understanding recursion and backtracking.

Applying constraint satisfaction problems.

Enhancing GUI development skills in C#.

Strengthening debugging and modular design skills.

# 🚀 Future Improvements
Add timer or move counter for challenge.

Expand to N > 12 for advanced users.

Integrate animations or sound effects for enhanced UX.
