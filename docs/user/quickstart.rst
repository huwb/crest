.. _quickstart:

Quickstart
==========

A video walk through of setting up the sample scene:

.. raw:: html

    <div style="margin:0 auto;width:90%">
        <iframe width="560" height="315" src="https://www.youtube.com/embed/qsgeG4sSLFw" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
    </div>

Installation
------------

Refer to :ref:`installation` for instructions on how to install **Crest**.

Adding the Ocean to a scene
---------------------------
* Create a new ``GameObject`` for the ocean component.
    
    * Assign the ``OceanRenderer`` component to it.
        
        * On startup this component generates the ocean geometry and does all the required initializations.

    * Assign the desired ocean material to the ``OceanRenderer`` script.
    * Set the Y coordinate of the ``GameObject`` to the desired sea level.

* Tag a primary camera as ``MainCamera`` or assign the ``Viewpoint`` transform on the ``OceanRenderer`` script.
  
  .. warning:: If you need to switch between multiple cameras, you must update the ``Viewpoint`` field to ensure the ocean follows the correct view.

* Be sure to generate lighting (:menuselection:`Window --> Rendering --> Lighting Settings --> Debug Settings --> Generate Lighting`)
  
  .. tip:: You can check **Auto Generate** to ensure lighting is always built.

Adding Waves
-------------
* Create a new ``GameObject`` for the waves component.

    * Assign the ``Shape Gerstner Batched`` component to it.

        * On startup this component creates a default ocean shape.
        * To edit the shape, create a ``Crest/Ocean Wave Spectrum`` and provide it instead.
        
        .. tip:: Smooth blending of ocean shapes can be achieved by adding  multiple ``Shape Gerstner Batched`` components and cross-fading them using the ``Weight`` parameter.

Conclusion
----------
And there you go! Now you have a pretty sweet looking ocean in your scene.