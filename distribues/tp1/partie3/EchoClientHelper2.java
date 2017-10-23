import java.net.*;
import java.io.*;

/**
 * This class is a module which provides the application logic for an Echo
 * client using stream-mode socket.
 * 
 * @author M. L. Liu
 */

public class EchoClientHelper2 {

	static final String endMessage = ".";
	private MyStreamSocket mySocket;
	private InetAddress serverHost;
	private int serverPort;
	final static String CMD_ADD = "ADD";
	final static String CMD_SUB = "SUB";
	final static String CMD_MUL = "MUL";
	final static String CMD_DIV = "DIV";

	EchoClientHelper2(String hostName, String portNum) throws SocketException, UnknownHostException, IOException {

		this.serverHost = InetAddress.getByName(hostName);
		this.serverPort = Integer.parseInt(portNum);
		// Instantiates a stream-mode socket and wait for a connection.
		this.mySocket = new MyStreamSocket(this.serverHost, this.serverPort);
		/**/ System.out.println("Connection request made");
	} // end constructor

	public String sendRequest(String message) throws SocketException, IOException {
		mySocket.sendMessage(message);

		String answer;
		String[] cmd = message.split(" ");
		if (cmd[0].equals(CMD_ADD)) {
			answer = "sum";
		} else if (cmd[0].equals(CMD_SUB)) {
			answer = "subtraction";
		} else if (cmd[0].equals(CMD_MUL)) {
			answer = "multiplication";
		} else if (cmd[0].equals(CMD_DIV)) {
			answer = "division";
		} else {
			answer = "echo";
		}
		System.out.println("thats a " + answer + " request \nresult: ");

		// now receive the echo
		String echo = mySocket.receiveMessage();
		return echo;
	} // end getEcho

	public void done() throws SocketException, IOException {
		mySocket.sendMessage(endMessage);
		mySocket.close();
	} // end done
} // end class
