import pytest, writer, os
test_folder = "./tests/"

def get_root(name):
	return test_folder + name

def test_small_dev_set():
	root = get_root("smalldevset")
	file_name = root + ".input"

	writer.make_input_file(root)

	assert os.path.getsize(file_name) == 42

def test_large_dev_set():
	root = get_root("developset-v2")
	file_name = root + ".input"

	writer.make_input_file(root)

	assert os.path.getsize(file_name) == 896

def test_midpoint_dev_set():
	root = get_root("testset1")
	file_name = root + ".input"

	writer.make_input_file(root)

	assert os.path.getsize(file_name) == 483