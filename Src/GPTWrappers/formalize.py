from openai import OpenAI


apiKeyVar  = ""

MODEL = "gpt-4o-mini"

client = OpenAI(api_key=apiKeyVar)

prompt = """
You are a GPT Wrapper that is being used for an application that can both formalize/informalize a segment of text.  The user will provide you with
some text, and it will be preceded by the words "formalize" or "informalize".  It will also contain a value, 0 to 5, that represents the degree to which you perform the requested action.  
For example "formalize 5" means to substantially formalize a text, as though in a incredibly professinal context, while
 "informalize 0" means to extremely subtly add some niceties to the text, and so on for other values.
The program will output your exact response, so please do not include any additional symbols, text, or quotation marks.  
Please try to keep the original flow and style of the paragraph (If there are spelling/grammatical errors, you may fix them)."""

messages = [ {"role": "system", "content": 
        prompt} ]

ExpCompResponse = input("Choose to Formalize or Informalize text\n")

while ExpCompResponse != "Formalize" and ExpCompResponse != "Informalize":
    ExpCompResponse = input("Please enter 'Formalize' or 'Informalize'\n")

ActionLevel = input("Choose a degree 0 to 5\n")

while (not ActionLevel.isnumeric()) or (ActionLevel.isnumeric() and (int(ActionLevel) > 5 or int(ActionLevel) < 0)):
    ActionLevel = input("Please enter a valid degree 0 to 5\n")

message = input("Please enter a message to expand/compress\n")
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