using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinderConsole
{
    public class WordFinder
    {
        private char[,] Matrix { get; set; }
        private int MatrixColumns { get; set; }
        private int MatrixRows { get; set; }

        private readonly int[] rowPositions = { -1,  0, 0, 1 };
        private readonly int[] columnPositions = {  0, -1, 1, 0 };

        /// <summary>
        /// WordFinder Constructor.
        /// </summary>
        /// <param name="matrix"></param>
        public WordFinder(IEnumerable<string> matrix)
        {
            ValidateConstructor(matrix);

            MatrixColumns = matrix.FirstOrDefault().Length;
            MatrixRows = matrix.Count();
            Matrix = new char[MatrixRows,MatrixColumns];

            //Index 
            int wordIndex = 0;

            //Add the wordStreamItems converted to a charArray into the Dictionary
            foreach (string word in matrix)
            {
                int letterIndex = 0;
                foreach (char letter in word.ToCharArray())
                {
                    Matrix[wordIndex, letterIndex] = letter;
                    letterIndex++;
                }
                wordIndex++;
            }

        }

        /// <summary>
        /// Find in the Matrix the Top 10 Words ordered by match count.
        /// </summary>
        /// <param name="wordstream"></param>
        /// <returns></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            List<WordFindResult> results = new List<WordFindResult>();

            //For each words
            foreach(string word in wordstream)
            {
                int wordMatches = FindWordMatches(word);
                if(wordMatches > 0)
                {
                    results.Add(new WordFindResult { Word = word, Count =  wordMatches});
                }
            }

            //Show top 10 words finded ordered by matches.
            return results.OrderByDescending(x => x.Count).Select(x => x.Word).Take(10);
        }

        /// <summary>
        /// Find how many times the Word was Found in the Matri
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int FindWordMatches(string word)
        {
            //Variable to store how many times the Word was found.
            int matches = 0;

            //Iterate all rows o the Matrix
            for (int row = 0; row < MatrixRows; row++)
            {
                //Iterate the columns
                for (int column = 0; column < MatrixColumns; column++)
                {
                    //Search the Word in every possible direction
                    if (Search(row, column, word))
                    {
                        matches++;
                    }
                }
            }

            return matches;
        }

        /// <summary>
        /// Searches for the word in the specified row/column of the Matrix in all possible positions.
        /// </summary>
        /// <param name="rowFrom"></param>
        /// <param name="columnFrom"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        private bool Search(int rowFrom, int columnFrom, string word)
        {
            // Verify the first character, no need to iterate if that doesn't match.
            if (Matrix[rowFrom, columnFrom] != word[0])
            {
                return false;
            }

            // Search the word in all possible directions
            for (int direction = 0; direction < 4; direction++)
            {

                // Position to search initialization
                int characterMatch;
                int rowDirection = rowFrom + rowPositions[direction];
                int columnDirection = columnFrom + columnPositions[direction];

                // Check all characters in current position, except the first one that was already checked before.
                for (characterMatch = 1; characterMatch < word.Length; characterMatch++)
                {
                    // Verify direction. Break if out of bounds.
                    if (rowDirection >= MatrixRows || rowDirection < 0 || columnDirection >= MatrixColumns || columnDirection < 0)
                    {
                        break;
                    }

                    // Verify if the word character was found in the direction. Break if not.
                    if (Matrix[rowDirection, columnDirection] != word[characterMatch])
                    {
                        break;
                    }

                    // Character was found!. Move to the next row/column direction.
                    rowDirection += rowPositions[direction];
                    columnDirection += columnPositions[direction];
                }

                // Verify all characters where found.
                if (characterMatch == word.Length)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Do some validations for the Matrix creation.
        /// </summary>
        /// <param name="matrixWordStreams"></param>
        private void ValidateConstructor(IEnumerable<string> matrixWordStreams)
        {
            //Verify if has values
            if (matrixWordStreams == null)
            {
                throw new Exception("The Matrix cant't be initialized with null values");
            }

            //Verify if has values
            if (!matrixWordStreams.Any())
            {
                throw new Exception("The Matrix must have at least one word.");
            }

            //Validate MaxLenght
            if (matrixWordStreams.Count() > 64)
            {
                throw new Exception("The matrix can't have more than 64 characters.");
            }

            //Take first string value lenght and verify if all have the same lenght
            int lenght = matrixWordStreams.FirstOrDefault().Length;

            if (matrixWordStreams.Any(x => x.Length != lenght))
            {
                throw new Exception("All matrix words should have the same lenght");
            }

        }

    }
}
