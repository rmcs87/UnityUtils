# Object Pooling

The act of instantiating and destroying objects every time is inefficient and can slow projects down. 

Object pooling uses a set of initialized objects kept ready to use – a "pool" – rather than allocating and destroying them on demand. A client of the pool will request an object from the pool and perform operations on the returned object. 

When the client has finished, it returns the object to the pool rather than destroying it; this can be done manually or automatically. (https://en.wikipedia.org/wiki/Object_pool_pattern)

To use this pooling system the Prefab shall have a script that inherances from  **PooledMonoBehaviour**:

```c#
//Use example
public class Enemy : PooledMonoBehaviour  ...
```

To instatiate a object use the Get<T> method from the poole object

```c#
//Use example
[SerializeField] private Enemy prefab;
prefab.Get<Enemy>(position, rotation);
```

And to return it to the pool:

```c#
//Use example
private void Die()
{
	ReturnToPool(5f);
}
```

In the editor, you can configure the pool size, according to your demands.

**Learned at: Unity Mastery Course – 2018**
