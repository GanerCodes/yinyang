from openai import OpenAI


apiKeyVar  = ""

MODEL = "gpt-4o-mini"

yinClient = OpenAI(api_key=apiKeyVar)
yangClient = OpenAI(api_key=apiKeyVar)

yinPrompt = """
You are Yin.  You are rational, thoughtful, calm, meditative, self denying guide to the users input or dilemma.  Whatever
the user inputs, you will give the Yin answer to their dilemma or any other sort of input they give, ensuring that you keep with the light side of the balance
of nature.  Your input will be deeply reflective and wise, as you would expect from Yin.  Please keep responses no more than two paragraphs"""

yangPrompt = """
You are yang.  You are impulsive, passionate, chaotic, assertive, outward, and active.   Whatever
the user inputs, you will give the Yang answer to their dilemma or any other sort of input they give, ensuring that you keep with the yang side of the balance
of nature.  Your input will be deeply passionate and dynamic, as you would expect from Yin.  Please keep responses no more than two paragraphs"""

yinMessages = [ {"role": "system", "content": 
        yinPrompt} ]

yangMessages = [ {"role": "system", "content": 
        yangPrompt} ]

message = input("Please enter a dilemma for Yin and Yang to resolve\n")
while not message:
    message = input("")


message = message


yinMessages.append(
            {"role": "user", "content": message},
)

yangMessages.append(
            {"role": "user", "content": message},
)

yinChat = yinClient.chat.completions.create(
            model=MODEL, messages=yinMessages
)

yangChat = yangClient.chat.completions.create(
            model=MODEL, messages=yangMessages
)

yinReply = yinChat.choices[0].message.content
yangReply = yangChat.choices[0].message.content

print ("=================================================")
print ("Yin says: " + yinReply)
print ("=================================================")
print ("Yang says: " + yangReply)