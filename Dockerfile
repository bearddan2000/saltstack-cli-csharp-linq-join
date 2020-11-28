FROM ubuntu:20.04

ARG IN_FILE="Program.cs"
ARG OUT_FILE="Program.exe"

COPY bin/ .

RUN apt-get update \
    && apt-get -y install software-properties-common \
    && add-apt-repository ppa:deadsnakes/ppa \
    && apt-get update \
    && apt-get -y install curl python3.7 git \
    && curl -L https://bootstrap.saltstack.com -o install_salt.sh \
    && sh install_salt.sh -P

COPY ./top.sls /srv/salt/

RUN git clone https://github.com/bearddan2000/saltstack-lib-pillars.git \
    && chmod -R +x saltstack-lib-pillars \
    && cp saltstack-lib-pillars/masterless.conf /etc/salt/minion.d \
    && cp saltstack-lib-pillars/lang/mono.sls /srv/salt \
    && rm -Rf saltstack-lib-pillars

RUN service salt-minion stop \
    && salt-call --local state.highstate

RUN chmod +x ${IN_FILE} \
    && mcs -o:${OUT_FILE} ${IN_FILE} \
    && chmod +x ${OUT_FILE}

CMD ["mono", "Program.exe"]
