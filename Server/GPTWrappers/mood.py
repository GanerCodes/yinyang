from openai import OpenAI


apiKeyVar  = ""

MODEL = "gpt-4o-mini"

client = OpenAI(api_key=apiKeyVar)

prompt = """
You are a GPT Wrapper that is being used for an application that can increase the positive or negative tone a segment of text.  The user will provide you with
some text, and it will be preceded by the words "Positive" or "Negative".  It will also contain a value, 0 to 5, that represents the degree to which you perform the requested action.  
For example "Positive 5" means to make a text substantially positive, friendly, and upbeat in nature, as though talking to a friend, while
 "Negative 0" means to extremely subtly add passively negative phrasing, and so on for other values.
The program will output your exact response, so please do not include any additional symbols, text, or quotation marks.  
Please try to keep the original flow and style of the paragraph (If there are spelling/grammatical errors, you may fix them)."""

messages = [ {"role": "system", "content": 
        prompt} ]

ExpCompResponse = input("Choose to Positive or Negative text\n")

while ExpCompResponse != "Positive" and ExpCompResponse != "Negative":
    ExpCompResponse = input("Please enter 'Positive' or 'Negative'\n")

ActionLevel = input("Choose a degree 0 to 5\n")

while (not ActionLevel.isnumeric()) or (ActionLevel.isnumeric() and (int(ActionLevel) > 5 or int(ActionLevel) < 0)):
    ActionLevel = input("Please enter a valid degree 0 to 5\n")

message = input("Please enter a message to make positive/negative\n")
while not message:
    message = input("")


message = ExpCompResponse + " " + ActionLevel + " - " + message


messages.append(
            {"role": "user", "content": message},
)

chat = client.chat.completions.create(
            model=MODEL, messages=messages
)

reply = chat.choices[0].message.content

print (reply)