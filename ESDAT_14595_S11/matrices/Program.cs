int[,] mat = new int[2, 3];

Random rand = new Random();
for (int f = 0;f < mat.GetLength(0); f++){
    for (int c = 0; c < mat.GetLength(1); c++) {
        mat[f, c] = rand.Next(1, 10);
    }
}

for (int f = 0; f < mat.GetLength(0); f++) {
    for (int c = 0; c < mat.GetLength(1); c++) {
        Console.Write("" + mat[f, c] +  ", ");
    }
    Console.WriteLine();
}


