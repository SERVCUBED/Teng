<!DOCTYPE html>
<html lang="en">
	<head>
		<meta charset="utf-8">
		<title>This sample page uses PHP</title>
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="application-name" content="Teng Project">
		<link href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css" rel="stylesheet">
		<link href="css/main.css" rel="stylesheet">
		<style>
		body {
	        background: #333333;
        }
        </style>
        
    
        <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
    </head>
    <body>
        <div class="wrapper">
            <ul>
                
                <li class=""><a href="index.html">Main Page</a></li>
                
                <li class=""><a href="other.html">This is the other page</a></li>
                
                <li class=""><a href="other2.html">Markdown Page</a></li>
                
                <li class="selected"><a href="phppage.php">This sample page uses PHP</a></li>
                
            </ul>
            
            <h1>This sample page uses PHP</h1>
            <?php echo 'This page uses PHP'; ?>
            
            <div class="footer">
                <p>This website was created with Teng Template Engine. Copyright &copy; some humans.</p>
            </div>
        </div>
    </body>
</html>