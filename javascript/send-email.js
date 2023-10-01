const sgMail = require("@sendgrid/mail");
sgMail.setApiKey("YOUR_API_KEY");

const main = () => {
	const msg = {
		to: "riyaghosalrg@gmail.com",
		from: "riya.ghosal@thoughtworks.com",
		subject: "Sending Email from Frontend JS",
		text: "This is the email content",
	};
	sgMail
		.send(msg)
		.then(() => {
			console.log("Email sent successfully");
		})
		.catch((error) => {
			console.error("Error sending email: ", error);
		});
};

main();
