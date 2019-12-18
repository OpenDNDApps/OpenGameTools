test: SHELL:=/bin/bash
test:
	bash <(curl -fsSL https://raw.githubusercontent.com/neogeek/unity-ci-tools/v1.0.0/bin/test.sh)

clean:
	git clean -xdf