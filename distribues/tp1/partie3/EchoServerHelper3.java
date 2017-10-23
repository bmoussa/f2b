import java.io.IOException;
import java.net.ServerSocket;


public class EchoServerHelper3 {
	private ServerSocket myConnectionSocket;
	private MyStreamSocket myDataSocket;
	final static String CMD_ADD = "ADD";
	final static String CMD_SUB = "SUB";
	final static String CMD_MUL = "MUL";
	final static String CMD_DIV = "DIV";

	EchoServerHelper3(int serverPort) throws IOException {
		this.myConnectionSocket = new ServerSocket(serverPort);
	}

	void acceptConnection() throws IOException {
		this.myDataSocket = new MyStreamSocket(myConnectionSocket.accept());
	}

	String receiveMessage() throws IOException {
		return myDataSocket.receiveMessage();
	}

	void closeConnection() throws IOException {
		myDataSocket.close();
	}

	void computeAnswer(String message) throws IOException {
		String answer;
		String[] cmd = message.split(" ");
		try {
			if (cmd[0].equals(CMD_ADD)) {
				answer = String.valueOf(Integer.parseInt(cmd[1]) + Integer.parseInt(cmd[2]));
			} else if (cmd[0].equals(CMD_SUB)) {
				answer = String.valueOf(Integer.parseInt(cmd[1]) - Integer.parseInt(cmd[2]));
			} else if (cmd[0].equals(CMD_MUL)) {
				answer = String.valueOf(Integer.parseInt(cmd[1]) * Integer.parseInt(cmd[2]));
			} else if (cmd[0].equals(CMD_DIV)) {
				answer = String.valueOf(Integer.parseInt(cmd[1]) / Integer.parseInt(cmd[2]));
			} else {
				answer = message;
			}
		} catch (ArrayIndexOutOfBoundsException e) {
			answer = "ArrayIndexOutOfBoundsException"; 
		}
		// Now send the echo to the requester
		myDataSocket.sendMessage(answer);
	}
} // end class
