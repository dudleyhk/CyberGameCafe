using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsToRemember : MonoBehaviour {

    private string[] theThings;
    private List<int> missedAnswers = new List<int>();

    public string getReason(int i)
    {
        return theThings[i];
    }

    void Awake()
    {
        theThings = new string[13];
        theThings[0] = "A government organisation such as HMRC will not contact citizens by e-mail nor by phone. "
            + "Correspondance between citizens and the government is almost always done by post or in person.";

        theThings[1] = "Most companies take care to use appropriate spelling and grammar when sending e-mails. You should be very wary of:"
            + "\n- Words or sentences written entirely in capital letters"
            + "\n-Misspelt words, especially in the name of the company"
            + "\n- Overuse of punctuation such as exclamation marks or question marks";

        theThings[2] = "If a company has business with you it is very likely they will have a record of your name,"
            + "if they start the e-mail with \"Dear customer\" or call you by your e-mail address,"
            + " the chances are that you shouldn't trust this e-mail.";

        theThings[3] = "You should always be careful with e-mails containing hyperlinks. Often they will be safe,"
            + "but if you're unsure you should check:\n- Hover over the link with your mouse cursor, this will tell"
            + " you where the link will take you, if this doesn't match what you're expecting, the e-mail is likely"
            + " fraudulent.\n- If you are using a device with a touch - screen and no mouse cursor, you can usually"
            + " check a link by pressing and holding the link until a menu comes up, then copying the link into a"
            + " browser bar.\n- Ensure you check the spelling on the link, if the company's name is misspelt then"
            + " you should definitely not click the link.";
            
        theThings[4] = "If the e-mail uses language that encourages you to act immediately, this can sometimes be"
            + " a sign that the sender is trying to prevent you from taking the time to consider the validity of what"
            + " they're saying. In particular you should be suspicious of an e-mail which includes threatening language.";

        theThings[5] = "Unless it's an e-mail you're expecting from someone you know well, an e-mail should have a subject"
            + " which indicates the content of the e-mail. If the e-mail was sent without a subject or if the subject"
            + " doesn't indicate why you're receiving the e-mail, you should be wary of its content.";

        theThings[6] = "If you receive an e-mail from someone in your contacts which seems unusually impersonal, especially if"
            + " it's from someone you haven't heard from in a long time, and the e-mail contains and unfamilliar"
            + " hyperlink, this is a common indicator that your friend's e-mail account has been hacked.You should e-mail"
            + " the sender to ask if they sent you the e-mail intentionally.";

        theThings[7] = "Almost every company you'll ever deal with will have their own e-mail domain (e.g. '@amazon.com')."
            + "\nIf the sender's e-mail address ends with a domain that provides e-mail addresses to the public (such as"
            + " '@gmail.com' or '@hotmail.co.uk') but the e-mail claims to be from a company you can usually assume that this"
            + " isn't a legitimate e-mail.";

        theThings[8] = "If an e-mail refers directly to something about you (e.g.a product you bought or an incident in which"
            + " you were involved), genuine e-mails will be able to provide some basic information about it such as which product"
            + " you bought or when the event took place.\nIf the e-mail makes a vague reference to something happening it is unlikely"
            + " to be genuine.";

        theThings[9] = "It is extremely uncommon for someone to win a competition they didn't enter. If you get an e-mail saying"
            + " you won a contest that you didn't enter it is almost always a scam.";

        theThings[10] = "If the content of the e-mail doesn't indicate who the sender is, how they know you or who they represent"
            + " then you should probably regard it as suspicious and avoid clicking any of the links.";

        theThings[11] = "If you have an account on a website and you receive an e-mail telling you about changes to your account"
            + " or something which requires action, if the e-mail is genuine then you will usually find indications of it on the"
            + " website itself.\nIt's often a good idea to visit the website in your browser rather than clicking hyperlinks in an e-mail.";

    }

    public void addMissedAnswer(int x)
    {
        missedAnswers.Add(x);
    }
}
