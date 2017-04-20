import subprocess,sys,mysql.connector

# Global Variables
# Taking the command line inputs and putting them into their respective variables
tier=int(sys.argv[1])
author = sys.argv[2]
title  = sys.argv[3]
#tier=3
#author='%'
#title='t'

conn = mysql.connector.connect(
    user='root',
    password='MaximumHaze16',
    host='localhost',
    database='seniorproject'
)
mycursor = conn.cursor()

def main():
    determine_query_type()

def determine_query_type():
    if(tier == 1):
        if(author is not "%" and title is "%"):
            print("Author Contains Search")
            author_contains_search(author)
        elif(author is "%" and title is not "%"):
            print("Title Contains Search")
            title_contains_search(title)
    elif(tier == 2):
        if (author is not "%" and title is "%"):
            print("Author Specific Search")
            author_specific_search(author)
        elif (author is "%" and title is not "%"):
            print("Title Specific Search")
            title_specific_search(title)
    elif(tier == 3):
        if (author is not "%" and title is "%"):
            print("Author Starts Search")
            author_starts_search(author)
        elif (author is "%" and title is not "%"):
            print("Title Starts Search")
            title_starts_search(title)
    else:
        print("No Recognized Search Requested")


def author_contains_search(author):
    authorQ='%'+author+'%'
    mycursor.execute("select matchedbooks.book_id,x.author_name,matchedbooks.title,matchedbooks.book_length from(select author_id,author_name from authors where author_name like %s) as x join matchedbooks on x.author_id = matchedbooks.author_id order by x.author_name asc",(authorQ,))
    output = mycursor.fetchall()
    with open("D:/Programs Files/Unity/Projects/Interactive Virtual Library Demo/outputfile.txt", 'w') as f:
              f.write(author + "%\n")
              for item in output:
                f.write(str(item[0])+"%"+item[1]+"%"+item[2]+"%"+ str(item[3])+"%\n")
    #fw = open("C:/Users/Sarah/Desktop/outputfile.txt", 'w')
    #fw.write(author + "%\n")
    #for item in output:
        #fw.write(str(item[0])+"%"+item[1]+"%%"+item[2]+"%%%"+ str(item[3])+"%%%%\n")
    #fw.close()

def author_specific_search(author):
    mycursor.execute("select matchedbooks.book_id,x.author_name,matchedbooks.title,matchedbooks.book_length from(select author_id,author_name from authors where author_name = %s) as x join matchedbooks on x.author_id = matchedbooks.author_id order by x.author_name asc",(author,))
    output = mycursor.fetchall()
    fw = open("outputfile.txt", 'w')
    fw.write(author + "%\n")
    for item in output:
        fw.write(str(item[0]) + "%" + item[1] + "%" + item[2] +"%"+ str(item[3])+"%\n")
    fw.close()

def author_starts_search(author):
    authorQ=author+'%'
    mycursor.execute("select matchedbooks.book_id,x.author_name,matchedbooks.title,matchedbooks.book_length from(select author_id,author_name from authors where author_name like %s) as x join matchedbooks on x.author_id = matchedbooks.author_id order by x.author_name asc",(authorQ,))
    output = mycursor.fetchall()
    fw = open("outputfile.txt", 'w')
    fw.write(author + "%\n")
    for item in output:
        fw.write(str(item[0]) + "%" + item[1] + "%" + item[2] +"%"+ str(item[3])+"%\n")
    fw.close()


def title_contains_search(title):
    titleQ = '%' + title + '%'
    mycursor.execute("select x.book_id,authors.author_name,x.title from(select book_id,title,author_id,matchedbooks.book_length from matchedbooks where title like %s) as x join authors on x.author_id=authors.author_id order by authors.author_name asc",(titleQ,))
    output = mycursor.fetchall()
    fw = open("outputfile.txt", 'w')
    fw.write(title + "%\n")
    for item in output:
        fw.write(str(item[0]) + "%" + item[1] + "%" + item[2] + "%"+ str(item[3])+"%\n")
    fw.close()


def title_specific_search(title):
    mycursor.execute("select x.book_id,authors.author_name,x.title from(select book_id,title,author_id,matchedbooks.book_length from matchedbooks where title = %s) as x join authors on x.author_id=authors.author_id order by authors.author_name asc",(title,))
    output = mycursor.fetchall()
    fw = open("outputfile.txt", 'w')
    fw.write(title + "%\n")
    for item in output:
        fw.write(str(item[0]) + "%" + item[1] + "%" + item[2] + "%"+ str(item[3])+"%\n")
    fw.close()

def title_starts_search(title):
    titleQ = title + '%'
    mycursor.execute("select x.book_id,authors.author_name,x.title from(select book_id,title,author_id,matchedbooks.book_length from matchedbooks where title like %s) as x join authors on x.author_id=authors.author_id order by authors.author_name asc",(titleQ,))
    output = mycursor.fetchall()
    fw = open("outputfile.txt", 'w')
    fw.write(title + "%\n")
    for item in output:
        fw.write(str(item[0]) + "%" + item[1] + "%" + item[2] + "%"+ str(item[3])+"%\n")
    fw.close()



if __name__ == '__main__':
    main()
