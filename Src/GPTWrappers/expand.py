from openai import OpenAI


apiKeyVar  = ""

MODEL = "gpt-4o-mini"

client = OpenAI(api_key=apiKeyVar)

prompt = """
You are a GPT Wrapper that is being used for an application that can both summarize/expand a segment of text.  The user will provide you with
some text, and it will be preceded by the words "summarize" or "expand".  It will also contain a value, 0 to 5, that represents the degree to which you perform the requested action.  
For example "Expand 5" means to substantially expand the given text, while "Compress 0" means to extremely subtly compress the text, and so on.
The program will output your exact response, so please do not include any additional symbols, text, or quotation marks.  
Please try to keep the original flow and style of the paragraph (If there are spelling/grammatical errors, you may fix them)."""

messages = [ {"role": "system", "content": 
        prompt} ]

ExpCompResponse = input("Choose to Expand or Compress text\n")

while ExpCompResponse != "Expand" and ExpCompResponse != "Compress":
    ExpCompResponse = input("Please enter 'Expand' or 'Compress'\n")

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