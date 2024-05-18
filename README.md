# CatTask
# Goal 
Create a command line application that saves a picture of a cat to disk. 

# Requirements: 
Retrieve a cat image from the following API: https://cataas.com/  
- This API supports overlaying text on the image, a feature your program will make use of (see arguments section 
        
# Support two arguments: 

- “-o output_filepath” : output file path to write the image to (required)  
- “-t text_to_overlay” : text to be included in the downloaded image (optional**) 

Cat image, including overlay text if specified, should be saved to location provided in the argument 

# Example 
The program should be called like this: mySampleApp.exe -o “file.jpg” -t “I like cats” 
