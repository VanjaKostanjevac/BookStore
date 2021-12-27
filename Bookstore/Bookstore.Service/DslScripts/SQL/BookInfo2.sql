SELECT 
	b.ID, 
	NumberOfTopics = count( t.BookID )
FROM 
	Bookstore.Book b
	RIGHT JOIN Bookstore.BookTopic t ON b.ID = t.BookID
GROUP BY 
	b.ID